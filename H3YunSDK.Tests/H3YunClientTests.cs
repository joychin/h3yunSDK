using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H3YunSDK.Configuration;
using H3YunSDK.Models;
using H3YunSDK.Models.BizObjects;
using H3YunSDK.Models.Workflow;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace H3YunSDK.Tests
{
    public class H3YunClientTests
    {
        private readonly Mock<IOptions<H3YunOptions>> _mockOptions;
        private readonly Mock<ILogger<H3YunClient>> _mockLogger;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly H3YunClient _client;

        public H3YunClientTests()
        {
            // 从配置文件读取配置
            var configOptions = TestConfiguration.GetH3YunOptions();
            
            // 配置模拟选项
            _mockOptions = new Mock<IOptions<H3YunOptions>>();
            _mockOptions.Setup(o => o.Value).Returns(configOptions);

            // 配置模拟日志记录器
            _mockLogger = new Mock<ILogger<H3YunClient>>();

            // 配置模拟HTTP处理程序
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

            // 创建客户端实例
            _client = new H3YunClient(
                _httpClient,
                _mockOptions.Object,
                logger: _mockLogger.Object);
        }

        [Fact]
        public async Task CreateBizObjectAsync_Success_ReturnsObjectId()
        {
            // 准备
            var request = new CreateBizObjectRequest
            {
                SchemaCode = "TestSchema",
                BizObject = new BizObject
                {
                    { "F0000001", "测试数据" }
                },
                IsSubmit = false
            };

            var expectedResponse = new ApiResponse<CreateBizObjectResponse>
            {
                Successful = true,
                ReturnData = new CreateBizObjectResponse
                {
                    ObjectId = "TestObjectId",
                    WorkflowInstanceId = "TestInstanceId"
                }
            };

            SetupMockHttpResponse(HttpStatusCode.OK, expectedResponse);

            // 执行
            var result = await _client.CreateBizObjectAsync(request);

            // 验证
            Assert.NotNull(result);
            Assert.True(result.Successful);
            Assert.NotNull(result.ReturnData);
            if (result?.ReturnData != null) {
                Assert.Equal("TestObjectId", result.ReturnData.ObjectId);
            }
            if (result?.ReturnData != null) {
                Assert.Equal("TestInstanceId", result.ReturnData.WorkflowInstanceId);
            }

            VerifyHttpRequestSent("/OpenApi/Invoke", HttpMethod.Post);
        }

        [Fact]
        public async Task LoadBizObjectAsync_Success_ReturnsBizObject()
        {
            // 准备
            var request = new LoadBizObjectRequest
            {
                SchemaCode = "D001262sm64549e003f4741388e6f734922faedbf",
                BizObjectId = "7320d456-02db-4aa2-b513-0e2f646a3180"
            };

            // 打印请求信息
            Console.WriteLine("=== 测试请求信息 ===");
            Console.WriteLine($"SchemaCode: {request.SchemaCode}");
            Console.WriteLine($"BizObjectId: {request.BizObjectId}");
            Console.WriteLine($"请求JSON: {JsonConvert.SerializeObject(request, Formatting.Indented)}");
            Console.WriteLine();

            var expectedResponse = new ApiResponse<LoadBizObjectResponse>
            {
                Successful = true,
                ReturnData = new LoadBizObjectResponse()
            };

            expectedResponse.ReturnData["gsmc"] = "氚云公司";

            // 打印期望响应信息
            Console.WriteLine("=== 期望响应信息 ===");
            Console.WriteLine($"响应JSON: {JsonConvert.SerializeObject(expectedResponse, Formatting.Indented)}");
            Console.WriteLine();

            SetupMockHttpResponse(HttpStatusCode.OK, expectedResponse);

            // 执行
            var result = await _client.LoadBizObjectAsync(request);

            // 打印实际响应信息
            Console.WriteLine("=== 实际响应信息 ===");
            Console.WriteLine($"Successful: {result.Successful}");
            Console.WriteLine($"gsmc: {result.ReturnData?["gsmc"]?.ToString()}");
            Console.WriteLine($"响应JSON: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
            Console.WriteLine();

            // 验证
            Assert.True(result.Successful);
            Assert.Equal("氚云公司", result.ReturnData?["gsmc"]?.ToString());

            VerifyHttpRequestSent("/OpenApi/Invoke", HttpMethod.Post);
        }

        [Fact]
        public async Task ListBizObjectsAsync_Success_ReturnsBizObjectsList()
        {
            // 准备
            var request = new ListBizObjectsRequest
            {
                SchemaCode = "TestSchema",
                Filter = new Filter
                {
                    FromRowNum = 0,
                    ToRowNum = 10
                }
            };

            var bizObjects = new List<BizObject>
            {
                new BizObject
                {
                    ObjectId = "TestObjectId1",
                    Name = "测试业务对象1"
                },
                new BizObject
                {
                    ObjectId = "TestObjectId2",
                    Name = "测试业务对象2"
                }
            };

            bizObjects[0]["F0000001"] = "测试数据1";
            bizObjects[1]["F0000001"] = "测试数据2";

            var expectedResponse = new ApiResponse<ListBizObjectsResponse>
            {
                Successful = true,
                ReturnData = new ListBizObjectsResponse
                {
                    BizObjects = bizObjects,
                    TotalCount = 2
                }
            };

            SetupMockHttpResponse(HttpStatusCode.OK, expectedResponse);

            // 执行
            var result = await _client.ListBizObjectsAsync(request);

            // 验证
            Assert.True(result.Successful);
            Assert.NotNull(result.ReturnData);
            var returnData = result.ReturnData!;
            Assert.Equal(2, returnData.TotalCount);
            
            Assert.NotNull(returnData.BizObjects);
            Assert.Collection(returnData.BizObjects,
                item => Assert.NotNull(item),
                item => Assert.NotNull(item));
            var bizObjectsList = returnData.BizObjects;
            Assert.NotNull(bizObjects[0]);
            Assert.Equal("TestObjectId1", bizObjects[0].ObjectId);
            Assert.Equal("测试数据1", bizObjects[0]["F0000001"]);

            VerifyHttpRequestSent("/OpenApi/Invoke", HttpMethod.Post);
        }

        [Fact]
        public async Task GetWorkflowInfoAsync_Success_ReturnsWorkflowInfo()
        {
            // 准备
            var request = new GetWorkflowInfoRequest
            {
                InstanceId = "TestInstanceId"
            };

            var expectedResponse = new ApiResponse<GetWorkflowInfoResponse>
            {
                Successful = true,
                ReturnData = new GetWorkflowInfoResponse
                {
                    InstanceId = "TestInstanceId",
                    WorkflowCode = "TestWorkflowCode",
                    WorkflowName = "测试工作流",
                    SchemaCode = "TestSchema",
                    BizObjectId = "TestObjectId",
                    Originator = "TestUserId",
                    OriginatorName = "测试用户",
                    StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = 2,
                    Activities = new List<ActivityInfo>
                    {
                        new ActivityInfo
                        {
                            ActivityId = "TestActivityId",
                            ActivityCode = "TestActivityCode",
                            ActivityName = "测试活动",
                            ActivityType = "Approve",
                            Participants = new List<Participant>
                            {
                                new Participant
                                {
                                    ParticipantId = "TestUserId",
                                    ParticipantName = "测试用户"
                                }
                            }
                        }
                    },
                    ApprovalLogs = new List<ApprovalLog>
                    {
                        new ApprovalLog
                        {
                            ActivityId = "TestActivityId",
                            ActivityCode = "TestActivityCode",
                            ActivityName = "测试活动",
                            ApproverID = "TestUserId",
                            ApproverName = "测试用户",
                            ApprovalTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            ApprovalResult = "Agree",
                            ApprovalComment = "同意"
                        }
                    }
                }
            };

            SetupMockHttpResponse(HttpStatusCode.OK, expectedResponse);

            // 执行
            var result = await _client.GetWorkflowInfoAsync(request);

            // 验证
            Assert.True(result.Successful);
            Assert.NotNull(result.ReturnData);
            var returnData = result.ReturnData;
            
            Assert.NotNull(returnData);
            if (returnData != null) {
                if (returnData != null) {
                Assert.NotNull(returnData.InstanceId);
            }
            }
            if (returnData != null) {
                Assert.Equal("TestInstanceId", returnData.InstanceId);
            }
            
            Assert.NotNull(returnData!.WorkflowName);
            Assert.Equal("测试工作流", returnData.WorkflowName!);
            
            Assert.NotNull(returnData!.Activities);
            var activities = returnData.Activities!;
            Assert.Single(activities);
            Assert.NotNull(activities[0]);
            var activity = activities[0]!;
            Assert.NotNull(activity.ActivityId);
            Assert.NotNull(activity.ActivityCode);
            Assert.NotNull(activity.ActivityName);
            Assert.NotNull(activity.ActivityType);
            Assert.NotNull(activity.Participants);
            
            var logs = returnData.ApprovalLogs;
            Assert.NotNull(logs);
            Assert.Single(logs);
            Assert.NotNull(logs);
            Assert.True(logs!.Count > 0);
            Assert.NotNull(logs[0]);
            var log = logs[0]!;
            Assert.NotNull(log.ActivityId);
            Assert.NotNull(log.ActivityCode);
            Assert.NotNull(log.ActivityName);

            VerifyHttpRequestSent("/OpenApi/Invoke", HttpMethod.Post);
        }

        [Fact]
        public async Task SubmitWorkflowAsync_Success_ReturnsInstanceId()
        {
            // 准备
            var request = new SubmitWorkflowRequest
            {
                InstanceId = "TestInstanceId",
                ApprovalAction = "Agree",
                Comment = "同意",
                UserId = "TestUserId"
            };

            var expectedResponse = new ApiResponse<SubmitWorkflowResponse>
            {
                Successful = true,
                ReturnData = new SubmitWorkflowResponse
                {
                    InstanceId = "TestInstanceId",
                    BizObjectId = "TestObjectId"
                }
            };

            SetupMockHttpResponse(HttpStatusCode.OK, expectedResponse);

            // 执行
            var result = await _client.SubmitWorkflowAsync(request);

            // 验证
            Assert.True(result.Successful);
            Assert.NotNull(result.ReturnData);
            var returnData = result.ReturnData!;
            
            Assert.NotNull(returnData.InstanceId);
            var instanceId = returnData.InstanceId!;
            Assert.Equal("TestInstanceId", instanceId);
            
            Assert.NotNull(returnData.BizObjectId);
            var bizObjectId = returnData.BizObjectId!;
            Assert.Equal("TestObjectId", bizObjectId);

            VerifyHttpRequestSent("/OpenApi/Invoke", HttpMethod.Post);
        }

        [Fact]
        public async Task InvokeCustomApiAsync_Success_ReturnsCustomResponse()
        {
            // 准备
            var request = new { CustomParam = "TestParam" };
            var expectedResponse = new ApiResponse<object>
            {
                Successful = true,
                ReturnData = new { Result = "Success" }
            };

            SetupMockHttpResponse(HttpStatusCode.OK, expectedResponse);

            // 执行
            var result = await _client.InvokeCustomApiAsync<object, object>("CustomMethod", request);

            // 验证
            Assert.True(result.Successful);
            Assert.NotNull(result.ReturnData);

            VerifyHttpRequestSent("/OpenApi/Invoke", HttpMethod.Post);
        }

        #region 辅助方法

        private void SetupMockHttpResponse<T>(HttpStatusCode statusCode, T responseContent)
        {
            var responseJson = JsonConvert.SerializeObject(responseContent);
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
                .Callback<HttpRequestMessage, CancellationToken>(async (request, cancellationToken) =>
                {
                    // 打印请求信息
                    Console.WriteLine("=== HTTP Request ===");
                    Console.WriteLine($"Method: {request.Method}");
                    Console.WriteLine($"URI: {request.RequestUri}");
                    Console.WriteLine("Headers:");
                    foreach (var header in request.Headers)
                    {
                        Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
                    }
                    if (request.Content != null)
                    {
                        foreach (var header in request.Content.Headers)
                        {
                            Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
                        }
                        var requestBody = await request.Content.ReadAsStringAsync();
                        Console.WriteLine($"Body: {requestBody}");
                    }
                    Console.WriteLine();
                })
                .ReturnsAsync(() =>
                {
                    // 打印响应信息
                    Console.WriteLine("=== HTTP Response ===");
                    Console.WriteLine($"Status: {response.StatusCode}");
                    Console.WriteLine("Headers:");
                    foreach (var header in response.Headers)
                    {
                        Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
                    }
                    if (response.Content != null)
                    {
                        foreach (var header in response.Content.Headers)
                        {
                            Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
                        }
                    }
                    Console.WriteLine($"Body: {responseJson}");
                    Console.WriteLine();
                    return response;
                });
        }

        private void VerifyHttpRequestSent(string expectedRequestUri, HttpMethod expectedMethod)
        {
            _mockHttpMessageHandler
                .Protected()
                .Verify<Task<HttpResponseMessage>>(
                    "SendAsync",
                    Times.AtLeastOnce(),
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req != null &&
                        req.Method == expectedMethod &&
                        req.RequestUri != null &&
                        req.RequestUri.AbsolutePath != null &&
                        req.RequestUri.AbsolutePath.EndsWith(expectedRequestUri)),
                    ItExpr.IsAny<CancellationToken>());
        }

        #endregion
    }
}