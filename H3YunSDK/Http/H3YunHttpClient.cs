using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H3YunSDK.Configuration;
using H3YunSDK.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace H3YunSDK.Http
{
    /// <summary>
    /// 氚云HTTP客户端
    /// </summary>
    internal class H3YunHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly H3YunOptions _options;
        private readonly ILogger<H3YunHttpClient>? _logger;

        /// <summary>
        /// 初始化氚云HTTP客户端
        /// </summary>
        /// <param name="httpClient">HTTP客户端</param>
        /// <param name="options">配置选项</param>
        /// <param name="logger">日志记录器</param>
        public H3YunHttpClient(
            HttpClient httpClient,
            IOptions<H3YunOptions> options,
            ILogger<H3YunHttpClient>? logger = null)
        {
            _options = options.Value;
            _options.Validate();
            
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            _logger = logger;
        }

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <typeparam name="TRequest">请求类型</typeparam>
        /// <typeparam name="TResponse">响应类型</typeparam>
        /// <param name="endpoint">API端点</param>
        /// <param name="request">请求数据</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>API响应</returns>
        public async Task<TResponse> PostAsync<TRequest, TResponse>(
            string endpoint,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger?.LogDebug("发送请求到 {Endpoint}", endpoint);
                
                var requestJson = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                
                // 添加身份认证参数
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint)
                {
                    Content = content
                };
                
                requestMessage.Headers.Add("EngineCode", _options.EngineCode);
                requestMessage.Headers.Add("EngineSecret", _options.EngineSecret);
                
                _logger?.LogDebug("请求内容: {RequestContent}", requestJson);
                
                using var response = await _httpClient.SendAsync(requestMessage, cancellationToken);
                
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger?.LogDebug("响应内容: {ResponseContent}", responseContent);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger?.LogError("HTTP请求失败，状态码：{StatusCode}，响应内容：{Response}",
                        response.StatusCode, responseContent);
                    throw new H3YunException($"HTTP请求失败，状态码：{response.StatusCode}", responseContent);
                }
                
                var result = JsonConvert.DeserializeObject<TResponse>(responseContent);
                if (result == null)
                {
                    throw new H3YunException("无法解析API响应", responseContent);
                }
                
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger?.LogError(ex, "HTTP请求异常: {Message}", ex.Message);
                throw new H3YunException($"HTTP请求异常: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger?.LogError(ex, "请求超时: {Message}", ex.Message);
                throw new H3YunException("请求超时", ex);
            }
            catch (JsonException ex)
            {
                _logger?.LogError(ex, "JSON解析异常: {Message}", ex.Message);
                throw new H3YunException($"JSON解析异常: {ex.Message}", ex);
            }
            catch (H3YunException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "未知异常: {Message}", ex.Message);
                throw new H3YunException($"未知异常: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="TResponse">响应类型</typeparam>
        /// <param name="uploadUrl">上传URL</param>
        /// <param name="fileBytes">文件字节数组</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentType">文件内容类型</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>上传响应</returns>
        public async Task<TResponse> UploadFileAsync<TResponse>(
            string uploadUrl,
            byte[] fileBytes,
            string fileName,
            string contentType,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger?.LogDebug("上传文件到 {UploadUrl}, 文件名: {FileName}", uploadUrl, fileName);
                
                var boundary = "----------" + DateTime.Now.Ticks.ToString("x");
                var boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                
                // 构建multipart/form-data内容
                var sb = new StringBuilder();
                sb.Append("--");
                sb.Append(boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data; name=\"media\";");
                sb.Append(" filename=\"");
                sb.Append(fileName);
                sb.Append("\"");
                sb.Append("\r\n");
                sb.Append("Content-Type: ");
                sb.Append(contentType + "\r\n\r\n");
                
                var postHeaderBytes = Encoding.UTF8.GetBytes(sb.ToString());
                
                using var content = new MultipartFormDataContent(boundary);
                using var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                content.Add(fileContent, "media", fileName);
                
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uploadUrl)
                {
                    Content = content
                };
                
                requestMessage.Headers.Add("EngineCode", _options.EngineCode);
                requestMessage.Headers.Add("EngineSecret", _options.EngineSecret);
                
                using var response = await _httpClient.SendAsync(requestMessage, cancellationToken);
                
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger?.LogDebug("上传响应内容: {ResponseContent}", responseContent);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger?.LogError("文件上传失败，状态码：{StatusCode}，响应内容：{Response}",
                        response.StatusCode, responseContent);
                    throw new H3YunException($"文件上传失败，状态码：{response.StatusCode}", responseContent);
                }
                
                var result = JsonConvert.DeserializeObject<TResponse>(responseContent);
                if (result == null)
                {
                    throw new H3YunException("无法解析上传响应", responseContent);
                }
                
                return result;
            }
            catch (H3YunException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "文件上传异常: {Message}", ex.Message);
                throw new H3YunException($"文件上传异常: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <typeparam name="TResponse">响应类型</typeparam>
        /// <param name="downloadUrl">下载URL</param>
        /// <param name="attachmentId">附件ID</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>下载响应</returns>
        public async Task<TResponse> DownloadFileAsync<TResponse>(
            string downloadUrl,
            string attachmentId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger?.LogDebug("下载文件从 {DownloadUrl}, 附件ID: {AttachmentId}", downloadUrl, attachmentId);
                
                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("attachmentId", attachmentId),
                    new KeyValuePair<string, string>("EngineCode", _options.EngineCode)
                };
                
                using var content = new FormUrlEncodedContent(formData);
                content.Headers.Add("EngineCode", _options.EngineCode);
                content.Headers.Add("EngineSecret", _options.EngineSecret);
                
                using var response = await _httpClient.PostAsync(downloadUrl, content, cancellationToken);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger?.LogError("文件下载失败，状态码：{StatusCode}，响应内容：{Response}",
                        response.StatusCode, errorContent);
                    throw new H3YunException($"文件下载失败，状态码：{response.StatusCode}", errorContent);
                }
                
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                var fileName = "";
                var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
                
                // 尝试从Content-Disposition头获取文件名
                if (response.Content.Headers.ContentDisposition != null)
                {
                    fileName = response.Content.Headers.ContentDisposition.FileName?.Trim('"') ?? "";
                }
                
                // 构建响应对象
                var downloadResponse = Activator.CreateInstance<TResponse>();
                if (downloadResponse is H3YunSDK.Models.BizObjects.DownloadBizObjectFileResponse fileResponse)
                {
                    fileResponse.FileBytes = fileBytes;
                    fileResponse.FileName = fileName;
                    fileResponse.ContentType = contentType;
                }
                
                return downloadResponse;
            }
            catch (H3YunException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "文件下载异常: {Message}", ex.Message);
                throw new H3YunException($"文件下载异常: {ex.Message}", ex);
            }
        }
    }
}