using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using H3YunSDK.Configuration;
using H3YunSDK.Http;
using H3YunSDK.Interfaces;
using H3YunSDK.Models;
using H3YunSDK.Models.BizObjects;
using H3YunSDK.Models.Workflow;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace H3YunSDK
{
    /// <summary>
    /// 氚云API客户端实现
    /// </summary>
    public class H3YunClient : IH3YunClient
    {
        private readonly H3YunHttpClient _httpClient;
        private readonly ILogger<H3YunClient>? _logger;
        private const string ApiEndpoint = "/OpenApi/Invoke";

        /// <summary>
        /// 初始化氚云API客户端
        /// </summary>
        /// <param name="httpClient">HTTP客户端</param>
        /// <param name="options">配置选项</param>
        /// <param name="logger">日志记录器</param>
        public H3YunClient(
            HttpClient httpClient,
            IOptions<H3YunOptions> options,
            ILogger<H3YunClient>? logger = null)
        {
            _httpClient = new H3YunHttpClient(httpClient, options);
            _logger = logger;
        }

        /// <summary>
        /// 创建业务对象
        /// </summary>
        /// <param name="request">创建业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>创建业务对象响应</returns>
        public async Task<ApiResponse<CreateBizObjectResponse>> CreateBizObjectAsync(
            CreateBizObjectRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用CreateBizObject接口，表单编码：{SchemaCode}", request.SchemaCode);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "CreateBizObject",
                SchemaCode = request.SchemaCode,
                BizObject = request.BizObject,
                IsSubmit = request.IsSubmit
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<CreateBizObjectResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 批量创建业务对象
        /// </summary>
        /// <param name="request">批量创建业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>批量创建业务对象响应</returns>
        public async Task<ApiResponse<CreateBizObjectsResponse>> CreateBizObjectsAsync(
            CreateBizObjectsRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用CreateBizObjects接口，表单编码：{SchemaCode}，批量创建数量：{Count}", 
                request.SchemaCode, request.BizObjectArray.Length);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "CreateBizObjects",
                SchemaCode = request.SchemaCode,
                BizObjectArray = request.BizObjectArray,
                IsSubmit = request.IsSubmit
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<CreateBizObjectsResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 加载业务对象
        /// </summary>
        /// <param name="request">加载业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>加载业务对象响应</returns>
        public async Task<ApiResponse<LoadBizObjectResponse>> LoadBizObjectAsync(
            LoadBizObjectRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用LoadBizObject接口，表单编码：{SchemaCode}，业务对象ID：{BizObjectId}",
                request.SchemaCode, request.BizObjectId);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "LoadBizObject",
                SchemaCode = request.SchemaCode,
                BizObjectId = request.BizObjectId
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<LoadBizObjectResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 更新业务对象
        /// </summary>
        /// <param name="request">更新业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>更新业务对象响应</returns>
        public async Task<ApiResponse<UpdateBizObjectResponse>> UpdateBizObjectAsync(
            UpdateBizObjectRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用UpdateBizObject接口，表单编码：{SchemaCode}", request.SchemaCode);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "UpdateBizObject",
                SchemaCode = request.SchemaCode,
                BizObject = request.BizObject,
                IsSubmit = request.IsSubmit
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<UpdateBizObjectResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 删除业务对象
        /// </summary>
        /// <param name="request">删除业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>删除业务对象响应</returns>
        public async Task<ApiResponse<RemoveBizObjectResponse>> RemoveBizObjectAsync(
            RemoveBizObjectRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用RemoveBizObject接口，表单编码：{SchemaCode}，业务对象ID：{BizObjectId}",
                request.SchemaCode, request.BizObjectId);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "RemoveBizObject",
                SchemaCode = request.SchemaCode,
                BizObjectId = request.BizObjectId
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<RemoveBizObjectResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 获取业务对象列表
        /// </summary>
        /// <param name="request">获取业务对象列表请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>获取业务对象列表响应</returns>
        public async Task<ApiResponse<ListBizObjectsResponse>> ListBizObjectsAsync(
            ListBizObjectsRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用ListBizObjects接口，表单编码：{SchemaCode}", request.SchemaCode);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "ListBizObjects",
                SchemaCode = request.SchemaCode,
                Filter = request.Filter
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<ListBizObjectsResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 批量加载业务对象
        /// </summary>
        /// <param name="request">批量加载业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>批量加载业务对象响应</returns>
        public async Task<ApiResponse<LoadBizObjectsResponse>> LoadBizObjectsAsync(
            LoadBizObjectsRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用LoadBizObjects接口，表单编码：{SchemaCode}", request.SchemaCode);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "LoadBizObjects",
                SchemaCode = request.SchemaCode,
                FilterString = request.Filter
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<LoadBizObjectsResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 批量更新业务对象
        /// </summary>
        /// <param name="request">批量更新业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>批量更新业务对象响应</returns>
        public async Task<ApiResponse<UpdateBizObjectsResponse>> UpdateBizObjectsAsync(
            UpdateBizObjectsRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用UpdateBizObjects接口，表单编码：{SchemaCode}，更新对象数量：{Count}",
                request.SchemaCode, request.BizObjectArray?.Length ?? 0);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "UpdateBizObjects",
                SchemaCode = request.SchemaCode,
                BizObjectArray = request.BizObjectArray,
                BizObjectIds = request.BizObjectIds
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<UpdateBizObjectsResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 批量删除业务对象
        /// </summary>
        /// <param name="request">批量删除业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>批量删除业务对象响应</returns>
        public async Task<ApiResponse<RemoveBizObjectsResponse>> RemoveBizObjectsAsync(
            RemoveBizObjectsRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用RemoveBizObjects接口，表单编码：{SchemaCode}，删除对象数量：{Count}",
                request.SchemaCode, request.BizObjectIds?.Length ?? 0);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "RemoveBizObjects",
                SchemaCode = request.SchemaCode,
                BizObjectIds = request.BizObjectIds
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<RemoveBizObjectsResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 获取工作流信息
        /// </summary>
        /// <param name="request">获取工作流信息请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>获取工作流信息响应</returns>
        public async Task<ApiResponse<GetWorkflowInfoResponse>> GetWorkflowInfoAsync(
            GetWorkflowInfoRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用GetWorkflowInfo接口，工作流实例ID：{InstanceId}", request.InstanceId);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "GetWorkflowInfo",
                InstanceId = request.InstanceId
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<GetWorkflowInfoResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 提交工作流
        /// </summary>
        /// <param name="request">提交工作流请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>提交工作流响应</returns>
        public async Task<ApiResponse<SubmitWorkflowResponse>> SubmitWorkflowAsync(
            SubmitWorkflowRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用SubmitWorkflow接口，工作流实例ID：{InstanceId}，审批动作：{ApprovalAction}",
                request.InstanceId, request.ApprovalAction);
            
            var apiRequest = new ApiRequest
            {
                ActionName = "SubmitWorkflow",
                InstanceId = request.InstanceId,
                ApprovalAction = request.ApprovalAction,
                Comment = request.Comment,
                UserId = request.UserId
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<SubmitWorkflowResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="request">上传附件请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>上传附件响应</returns>
        public async Task<ApiResponse<UploadAttachmentResponse>> UploadAttachmentAsync(
            UploadAttachmentRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("上传附件：{SchemaCode}, {FilePropertyName}, {BizObjectId}, {FileName}", 
                request.SchemaCode, request.FilePropertyName, request.BizObjectId, request.FileName);
            
            // 构建上传URL
            var uploadUrl = $"https://www.h3yun.com/OpenApi/UploadAttachment?SchemaCode={request.SchemaCode}&FilePropertyName={request.FilePropertyName}&BizObjectId={request.BizObjectId}";
            
            var response = await _httpClient.UploadFileAsync<UploadAttachmentResponse>(
                uploadUrl, request.FileBytes, request.FileName, request.ContentType, cancellationToken);
                
            return new ApiResponse<UploadAttachmentResponse>
            {
                Successful = true,
                ReturnData = response
            };
        }

        /// <summary>
        /// 下载业务对象文件
        /// </summary>
        /// <param name="request">下载业务对象文件请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>下载业务对象文件响应</returns>
        public async Task<ApiResponse<DownloadBizObjectFileResponse>> DownloadBizObjectFileAsync(
            DownloadBizObjectFileRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("下载业务对象文件：{AttachmentId}", request.AttachmentId);
            
            var downloadUrl = "https://www.h3yun.com/Api/DownloadBizObjectFile";
            
            var response = await _httpClient.DownloadFileAsync<DownloadBizObjectFileResponse>(
                downloadUrl, request.AttachmentId, cancellationToken);
                
            return new ApiResponse<DownloadBizObjectFileResponse>
            {
                Successful = true,
                ReturnData = response
            };
        }

        /// <summary>
        /// 调用自定义接口
        /// </summary>
        /// <typeparam name="TRequest">请求类型</typeparam>
        /// <typeparam name="TResponse">响应类型</typeparam>
        /// <param name="method">接口方法名</param>
        /// <param name="request">请求数据</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>自定义接口响应</returns>
        public async Task<ApiResponse<TResponse>> InvokeCustomApiAsync<TRequest, TResponse>(
            string method,
            TRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("调用自定义接口：{Method}", method);
            
            var apiRequest = new ApiRequest
            {
                ActionName = method,
                CustomData = request
            };
            
            return await _httpClient.PostAsync<ApiRequest, ApiResponse<TResponse>>(
                ApiEndpoint, apiRequest, cancellationToken);
        }
    }

    /// <summary>
    /// API请求
    /// </summary>
    internal class ApiRequest
    {
        /// <summary>
        /// 接口方法名
        /// </summary>
        public string ActionName { get; set; } = string.Empty;

        /// <summary>
        /// 表单编码
        /// </summary>
        public string? SchemaCode { get; set; }

        /// <summary>
        /// 业务对象ID
        /// </summary>
        public string? BizObjectId { get; set; }

        /// <summary>
        /// 业务对象数据
        /// </summary>
        public BizObject? BizObject { get; set; }

        /// <summary>
        /// 业务对象数组（用于批量创建）
        /// </summary>
        public string[]? BizObjectArray { get; set; }

        /// <summary>
        /// 业务对象ID数组（用于批量更新和删除）
        /// </summary>
        public string[]? BizObjectIds { get; set; }

        /// <summary>
        /// 是否提交
        /// </summary>
        public bool? IsSubmit { get; set; }

        /// <summary>
        /// 过滤条件（对象类型，用于ListBizObjects等接口）
        /// </summary>
        public Filter? Filter { get; set; }

        /// <summary>
        /// 过滤条件（字符串类型，用于LoadBizObjects等接口）
        /// </summary>
        public string? FilterString { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public string? InstanceId { get; set; }

        /// <summary>
        /// 审批动作
        /// </summary>
        public string? ApprovalAction { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public object? CustomData { get; set; }
    }
}