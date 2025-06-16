using H3YunSDK;
using H3YunSDK.Models.BizObjects;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace H3YunSDK.Examples
{
    /// <summary>
    /// UpdateBizObjects和RemoveBizObjects接口使用示例
    /// </summary>
    public class UpdateAndRemoveBizObjectsExample
    {
        private readonly IH3YunClient _h3YunClient;
        private readonly ILogger<UpdateAndRemoveBizObjectsExample> _logger;

        public UpdateAndRemoveBizObjectsExample(IH3YunClient h3YunClient, ILogger<UpdateAndRemoveBizObjectsExample> logger)
        {
            _h3YunClient = h3YunClient;
            _logger = logger;
        }

        /// <summary>
        /// 批量更新业务数据示例
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <param name="objectIds">要更新的对象ID数组</param>
        /// <returns></returns>
        public async Task<List<string>?> UpdateBizObjectsExampleAsync(string schemaCode, string[] objectIds)
        {
            try
            {
                // 构建更新的业务对象数据（JSON字符串数组）
                var bizObjectJsonArray = new string[]
                {
                    JsonSerializer.Serialize(new
                    {
                        F0000002 = "更新后的数据1",
                        F0000009 = "03ea2021-f7d5-4001-b996-7115e63f4319"
                    }),
                    JsonSerializer.Serialize(new
                    {
                        F0000002 = "更新后的数据2",
                        F0000009 = "6d1af175-a49d-48ad-bc3e-52aa35bb34df"
                    })
                };

                // 创建请求
                var request = new UpdateBizObjectsRequest
                {
                    SchemaCode = schemaCode,
                    BizObjectArray = bizObjectJsonArray,
                    BizObjectIds = objectIds
                };

                // 调用接口
                var response = await _h3YunClient.UpdateBizObjectsAsync(request);

                if (response.Successful && response.ReturnData?.ObjectIds != null)
                {
                    _logger.LogInformation("成功批量更新 {Count} 个业务对象", response.ReturnData.ObjectIds.Count);
                    
                    // 输出更新的对象ID
                    foreach (var objectId in response.ReturnData.ObjectIds)
                    {
                        _logger.LogInformation("更新的业务对象ID：{ObjectId}", objectId);
                    }
                    
                    return response.ReturnData.ObjectIds;
                }
                else
                {
                    _logger.LogError("批量更新业务对象失败：{ErrorMessage}", response.ErrorMessage);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "调用UpdateBizObjects接口时发生异常");
                throw;
            }
        }

        /// <summary>
        /// 批量删除业务数据示例
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <param name="objectIds">要删除的对象ID数组</param>
        /// <returns></returns>
        public async Task<List<string>?> RemoveBizObjectsExampleAsync(string schemaCode, string[] objectIds)
        {
            try
            {
                // 创建请求
                var request = new RemoveBizObjectsRequest
                {
                    SchemaCode = schemaCode,
                    BizObjectIds = objectIds
                };

                // 调用接口
                var response = await _h3YunClient.RemoveBizObjectsAsync(request);

                if (response.Successful && response.ReturnData?.ObjectIds != null)
                {
                    _logger.LogInformation("成功批量删除 {Count} 个业务对象", response.ReturnData.ObjectIds.Count);
                    
                    // 输出删除的对象ID
                    foreach (var objectId in response.ReturnData.ObjectIds)
                    {
                        _logger.LogInformation("删除的业务对象ID：{ObjectId}", objectId);
                    }
                    
                    return response.ReturnData.ObjectIds;
                }
                else
                {
                    _logger.LogError("批量删除业务对象失败：{ErrorMessage}", response.ErrorMessage);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "调用RemoveBizObjects接口时发生异常");
                throw;
            }
        }

        /// <summary>
        /// 批量操作完整流程示例：创建 -> 更新 -> 删除
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <returns></returns>
        public async Task<bool> CompleteWorkflowExampleAsync(string schemaCode)
        {
            try
            {
                // 1. 批量创建业务对象
                var createRequest = new CreateBizObjectsRequest
                {
                    SchemaCode = schemaCode,
                    BizObjectArray = new string[]
                    {
                        JsonSerializer.Serialize(new
                        {
                            CreatedBy = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                            OwnerId = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                            F0000002 = "初始数据1",
                            F0000009 = "03ea2021-f7d5-4001-b996-7115e63f4319"
                        }),
                        JsonSerializer.Serialize(new
                        {
                            CreatedBy = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                            OwnerId = "f3f69a49-edf6-468d-9aee-8cbc82a46662",
                            F0000002 = "初始数据2",
                            F0000009 = "6d1af175-a49d-48ad-bc3e-52aa35bb34df"
                        })
                    },
                    IsSubmit = false
                };

                var createResponse = await _h3YunClient.CreateBizObjectsAsync(createRequest);
                if (!createResponse.Successful || createResponse.ReturnData?.ObjectIds == null)
                {
                    _logger.LogError("创建业务对象失败");
                    return false;
                }

                var objectIds = createResponse.ReturnData.ObjectIds.ToArray();
                _logger.LogInformation("成功创建 {Count} 个业务对象", objectIds.Length);

                // 2. 批量更新业务对象
                var updateResult = await UpdateBizObjectsExampleAsync(schemaCode, objectIds);
                if (updateResult == null)
                {
                    _logger.LogError("更新业务对象失败");
                    return false;
                }

                // 3. 批量删除业务对象
                var removeResult = await RemoveBizObjectsExampleAsync(schemaCode, objectIds);
                if (removeResult == null)
                {
                    _logger.LogError("删除业务对象失败");
                    return false;
                }

                _logger.LogInformation("完整的批量操作流程执行成功");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "执行完整工作流程时发生异常");
                return false;
            }
        }

        /// <summary>
        /// 使用BizObject对象构建更新数据的辅助方法
        /// </summary>
        /// <param name="bizObjects">业务对象列表</param>
        /// <returns>JSON字符串数组</returns>
        public static string[] ConvertBizObjectsToJsonArray(List<BizObject> bizObjects)
        {
            return bizObjects.Select(obj => JsonSerializer.Serialize(obj)).ToArray();
        }

        /// <summary>
        /// 从现有对象中提取ID数组的辅助方法
        /// </summary>
        /// <param name="bizObjects">业务对象列表</param>
        /// <returns>对象ID数组</returns>
        public static string[] ExtractObjectIds(List<BizObject> bizObjects)
        {
            return bizObjects.Where(obj => !string.IsNullOrEmpty(obj.ObjectId))
                           .Select(obj => obj.ObjectId!)
                           .ToArray();
        }
    }
}