using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H3YunSDK.Configuration;
using H3YunSDK.Exceptions;
using H3YunSDK.Http;
using H3YunSDK.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace H3YunSDK.Tests.Http
{
    public class H3YunHttpClientTests
    {
        private readonly Mock<IOptions<H3YunOptions>> _mockOptions;
        private readonly Mock<ILogger<H3YunHttpClient>> _mockLogger;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly H3YunHttpClient _client;

        public H3YunHttpClientTests()
        {
            // 从配置文件读取配置
            var configOptions = TestConfiguration.GetH3YunOptions();
            
            // 配置模拟选项
            _mockOptions = new Mock<IOptions<H3YunOptions>>();
            _mockOptions.Setup(o => o.Value).Returns(configOptions);

            // 配置模拟日志记录器
            _mockLogger = new Mock<ILogger<H3YunHttpClient>>();

            // 配置模拟HTTP处理程序
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.h3yun.com")
            };

            // 创建客户端实例
            _client = new H3YunHttpClient(_httpClient, _mockOptions.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task PostAsync_Success_ReturnsDeserializedResponse()
        {
            // 准备
            var request = new { TestProperty = "TestValue" };
            var expectedResponse = new ApiResponse<TestResponse>
            {
                Successful = true,
                ReturnData = new TestResponse { Result = "Success" }
            };

            SetupMockHttpResponse(HttpStatusCode.OK, expectedResponse);

            // 执行
            var result = await _client.PostAsync<object, ApiResponse<TestResponse>>("/test", request);

            // 验证
            Assert.True(result.Successful);
            Assert.NotNull(result.ReturnData);
            if (result?.ReturnData != null) {
                Assert.Equal("Success", result.ReturnData.Result);
            }

            VerifyHttpRequestSent("/test", HttpMethod.Post);
        }

        [Fact]
        public async Task PostAsync_HttpError_ThrowsH3YunException()
        {
            // 准备
            var request = new { TestProperty = "TestValue" };

            SetupMockHttpResponse(HttpStatusCode.BadRequest, "Bad Request");

            // 执行 & 验证
            var exception = await Assert.ThrowsAsync<H3YunException>(
                () => _client.PostAsync<object, ApiResponse<TestResponse>>("/test", request));

            Assert.Contains("HTTP请求失败", exception.Message);
            Assert.Equal("Bad Request", exception.ErrorCode);

            VerifyHttpRequestSent("/test", HttpMethod.Post);
        }

        [Fact]
        public async Task PostAsync_JsonDeserializationError_ThrowsH3YunException()
        {
            // 准备
            var request = new { TestProperty = "TestValue" };

            // 设置无效的JSON响应
            SetupMockHttpResponse(HttpStatusCode.OK, "Invalid JSON");

            // 执行 & 验证
            var exception = await Assert.ThrowsAsync<H3YunException>(
                () => _client.PostAsync<object, ApiResponse<TestResponse>>("/test", request));

            Assert.Contains("JSON", exception.Message);

            VerifyHttpRequestSent("/test", HttpMethod.Post);
        }

        [Fact]
        public async Task PostAsync_NullResponse_ThrowsH3YunException()
        {
            // 准备
            var request = new { TestProperty = "TestValue" };

            // 设置空响应
            SetupMockHttpResponse(HttpStatusCode.OK, "");

            // 执行 & 验证
            var exception = await Assert.ThrowsAsync<H3YunException>(
                () => _client.PostAsync<object, ApiResponse<TestResponse>>("/test", request));

            Assert.Contains("无法解析API响应", exception.Message);

            VerifyHttpRequestSent("/test", HttpMethod.Post);
        }

        [Fact]
        public async Task PostAsync_TimeoutException_ThrowsH3YunException()
        {
            // 准备
            var request = new { TestProperty = "TestValue" };

            // 设置超时异常
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new TaskCanceledException("The request timed out", new TimeoutException()));

            // 执行 & 验证
            var exception = await Assert.ThrowsAsync<H3YunException>(
                () => _client.PostAsync<object, ApiResponse<TestResponse>>("/test", request));

            Assert.Contains("请求超时", exception.Message);
        }

        [Fact]
        public async Task PostAsync_HttpRequestException_ThrowsH3YunException()
        {
            // 准备
            var request = new { TestProperty = "TestValue" };

            // 设置HTTP请求异常
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Network error"));

            // 执行 & 验证
            var exception = await Assert.ThrowsAsync<H3YunException>(
                () => _client.PostAsync<object, ApiResponse<TestResponse>>("/test", request));

            Assert.Contains("HTTP请求异常", exception.Message);
        }

        [Fact]
        public async Task PostAsync_GenericException_ThrowsH3YunException()
        {
            // 准备
            var request = new { TestProperty = "TestValue" };

            // 设置通用异常
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new Exception("Generic error"));

            // 执行 & 验证
            var exception = await Assert.ThrowsAsync<H3YunException>(
                () => _client.PostAsync<object, ApiResponse<TestResponse>>("/test", request));

            Assert.Contains("未知异常", exception.Message);
        }

        #region 辅助方法

        private void SetupMockHttpResponse<T>(HttpStatusCode statusCode, T responseContent)
        {
            string responseJson;
            if (responseContent is string stringContent)
            {
                responseJson = stringContent;
            }
            else
            {
                responseJson = JsonConvert.SerializeObject(responseContent);
            }

            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
        }

        private void VerifyHttpRequestSent(string expectedRequestUri, HttpMethod expectedMethod)
        {
            _mockHttpMessageHandler
                .Protected()
                .Verify<Task<HttpResponseMessage>>(
                    "SendAsync",
                    Times.AtLeastOnce(),
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == expectedMethod &&
                        req.RequestUri != null &&
                        req.RequestUri.ToString().EndsWith(expectedRequestUri)),
                    ItExpr.IsAny<CancellationToken>());
        }

        private class TestResponse
        {
            public string Result { get; set; } = string.Empty;
        }

        #endregion
    }
}