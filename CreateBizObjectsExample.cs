using H3YunSDK;
using H3YunSDK.Models.BizObjects;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace H3YunSDK.Examples
{
    /// <summary>
    /// CreateBizObjects接口使用示例
    /// </summary>
    public class CreateBizObjectsExample
    {
        private readonly IH3YunClient _h3YunClient;
        private readonly ILogger<CreateBizObjectsExample> _logger;

        public CreateBizObjectsExample(IH3YunClient h3YunClient, ILogger<CreateBizObjectsExample> logger)
        {
            _h3YunClient = h3YunClient;
            _logger = logger;
        }

        /// <summary>
        /// 批量创建业务数据示例
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <returns></returns>
        public async Task<List<string>?> CreateBizObjectsExampleAsync(string schemaCode)
        {
            try
            {
                // 构建业务对象数据（JSON字符串数组）
                var bizObjectJsonArray = new string[]
                {
                    JsonSerializer.Serialize(new
                    {
                        CreatedBy = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                        OwnerId = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                        F0000002 = "测试数据1",
                        F0000009 = "03ea2021-f7d5-4001-b996-7115e63f4319"
                    }),
                    JsonSerializer.Serialize(new
                    {
                        CreatedBy = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                        OwnerId = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                        F0000002 = "测试数据2",
                        F0000009 = "6d1af175-a49d-48ad-bc3e-52aa35bb34df"
                    })
                };

                // 创建请求
                var request = new CreateBizObjectsRequest
                {
                    SchemaCode = schemaCode,
                    BizObjectArray = bizObjectJsonArray,
                    IsSubmit = false // 是否提交工作流
                };

                // 调用接口
                var response = await _h3YunClient.CreateBizObjectsAsync(request);

                if (response.Successful && response.ReturnData?.ObjectIds != null)
                {
                    _logger.LogInformation("成功批量创建 {Count} 个业务对象", response.ReturnData.ObjectIds.Count);
                    
                    // 输出创建的对象ID
                    foreach (var objectId in response.ReturnData.ObjectIds)
                    {
                        _logger.LogInformation("创建的业务对象ID：{ObjectId}", objectId);
                    }
                    
                    return response.ReturnData.ObjectIds;
                }
                else
                {
                    _logger.LogError("批量创建业务对象失败：{ErrorMessage}", response.ErrorMessage);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "调用CreateBizObjects接口时发生异常");
                throw;
            }
        }

        /// <summary>
        /// 批量创建并提交工作流示例
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <returns></returns>
        public async Task<(List<string>? ObjectIds, List<string>? WorkflowInstanceIds)> CreateAndSubmitBizObjectsAsync(string schemaCode)
        {
            try
            {
                // 构建包含子表数据的业务对象
                var bizObjectWithSubTable = new
                {
                    CreatedBy = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                    OwnerId = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                    F0000002 = "主表数据",
                    F0000009 = "03ea2021-f7d5-4001-b996-7115e63f4319",
                    // 子表数据示例
                    D000024Fdetail123 = new object[]
                    {
                        new { zh = "子表行1" },
                        new { zh = "子表行2" }
                    }
                };

                var bizObjectJsonArray = new string[]
                {
                    JsonSerializer.Serialize(bizObjectWithSubTable)
                };

                // 创建请求并提交工作流
                var request = new CreateBizObjectsRequest
                {
                    SchemaCode = schemaCode,
                    BizObjectArray = bizObjectJsonArray,
                    IsSubmit = true // 提交工作流
                };

                // 调用接口
                var response = await _h3YunClient.CreateBizObjectsAsync(request);

                if (response.Successful && response.ReturnData != null)
                {
                    _logger.LogInformation("成功批量创建并提交 {Count} 个业务对象", 
                        response.ReturnData.ObjectIds?.Count ?? 0);
                    
                    if (response.ReturnData.WorkflowInstanceIds != null)
                    {
                        foreach (var workflowId in response.ReturnData.WorkflowInstanceIds)
                        {
                            _logger.LogInformation("工作流实例ID：{WorkflowInstanceId}", workflowId);
                        }
                    }
                    
                    return (response.ReturnData.ObjectIds, response.ReturnData.WorkflowInstanceIds);
                }
                else
                {
                    _logger.LogError("批量创建并提交业务对象失败：{ErrorMessage}", response.ErrorMessage);
                    return (null, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "调用CreateBizObjects接口时发生异常");
                throw;
            }
        }

        /// <summary>
        /// 使用BizObject对象构建JSON字符串的辅助方法
        /// </summary>
        /// <param name="bizObjects">业务对象列表</param>
        /// <returns>JSON字符串数组</returns>
        public static string[] ConvertBizObjectsToJsonArray(List<BizObject> bizObjects)
        {
            return bizObjects.Select(obj => JsonSerializer.Serialize(obj)).ToArray();
        }
    }
}