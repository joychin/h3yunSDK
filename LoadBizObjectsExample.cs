using H3YunSDK;
using H3YunSDK.Models.BizObjects;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace H3YunSDK.Examples
{
    /// <summary>
    /// LoadBizObjects接口使用示例
    /// </summary>
    public class LoadBizObjectsExample
    {
        private readonly IH3YunClient _h3YunClient;
        private readonly ILogger<LoadBizObjectsExample> _logger;

        public LoadBizObjectsExample(IH3YunClient h3YunClient, ILogger<LoadBizObjectsExample> logger)
        {
            _h3YunClient = h3YunClient;
            _logger = logger;
        }

        /// <summary>
        /// 批量查询业务数据示例
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <returns></returns>
        public async Task<List<BizObject>?> LoadBizObjectsExampleAsync(string schemaCode)
        {
            try
            {
                // 构建过滤条件（JSON字符串格式）
                var filter = new
                {
                    FromRowNum = 0,
                    ToRowNum = 500,
                    RequireCount = false,
                    ReturnItems = new string[] { }, // 空数组表示返回所有字段
                    SortByCollection = new object[] { }, // 排序字段，目前不支持
                    Matcher = new
                    {
                        Type = "And",
                        Matchers = new object[]
                        {
                            new
                            {
                                Type = "Item",
                                Name = "SeqNo", // 字段名
                                Operator = 2, // 运算符：2=等于
                                Value = "201900000001" // 值
                            }
                        }
                    }
                };

                var filterJson = JsonSerializer.Serialize(filter);

                // 创建请求
                var request = new LoadBizObjectsRequest
                {
                    SchemaCode = schemaCode,
                    Filter = filterJson
                };

                // 调用接口
                var response = await _h3YunClient.LoadBizObjectsAsync(request);

                if (response.Successful && response.ReturnData?.BizObjectArray != null)
                {
                    _logger.LogInformation("成功获取到 {Count} 条业务数据", response.ReturnData.BizObjectArray.Count);
                    return response.ReturnData.BizObjectArray;
                }
                else
                {
                    _logger.LogError("获取业务数据失败：{ErrorMessage}", response.ErrorMessage);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "调用LoadBizObjects接口时发生异常");
                throw;
            }
        }

        /// <summary>
        /// 复杂查询条件示例
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <returns></returns>
        public async Task<List<BizObject>?> LoadBizObjectsWithComplexFilterAsync(string schemaCode)
        {
            try
            {
                // 构建复杂的过滤条件
                var filter = new
                {
                    FromRowNum = 0,
                    ToRowNum = 100,
                    RequireCount = true,
                    ReturnItems = new[] { "ObjectId", "Name", "CreatedTime" }, // 只返回指定字段
                    SortByCollection = new object[] { },
                    Matcher = new
                    {
                        Type = "And",
                        Matchers = new object[]
                        {
                            // 条件1：SeqNo = '201900000001'
                            new
                            {
                                Type = "Item",
                                Name = "SeqNo",
                                Operator = 2, // 等于
                                Value = "201900000001"
                            },
                            // 条件2：Title = '标题1'
                            new
                            {
                                Type = "Item",
                                Name = "Title",
                                Operator = 2, // 等于
                                Value = "标题1"
                            },
                            // 条件3：OR条件 - Like包含'选项2'
                            new
                            {
                                Type = "Or",
                                Matchers = new object[]
                                {
                                    new
                                    {
                                        Type = "Item",
                                        Name = "Like",
                                        Operator = 8, // 包含
                                        Value = "选项2"
                                    }
                                }
                            }
                        }
                    }
                };

                var filterJson = JsonSerializer.Serialize(filter);

                var request = new LoadBizObjectsRequest
                {
                    SchemaCode = schemaCode,
                    Filter = filterJson
                };

                var response = await _h3YunClient.LoadBizObjectsAsync(request);

                if (response.Successful && response.ReturnData?.BizObjectArray != null)
                {
                    _logger.LogInformation("成功获取到 {Count} 条业务数据，总数：{TotalCount}", 
                        response.ReturnData.BizObjectArray.Count, response.ReturnData.TotalCount);
                    return response.ReturnData.BizObjectArray;
                }
                else
                {
                    _logger.LogError("获取业务数据失败：{ErrorMessage}", response.ErrorMessage);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "调用LoadBizObjects接口时发生异常");
                throw;
            }
        }
    }

    /// <summary>
    /// 运算符常量
    /// </summary>
    public static class FilterOperators
    {
        /// <summary>
        /// 大于
        /// </summary>
        public const int GreaterThan = 0;

        /// <summary>
        /// 大于等于
        /// </summary>
        public const int GreaterThanOrEqual = 1;

        /// <summary>
        /// 等于
        /// </summary>
        public const int Equal = 2;

        /// <summary>
        /// 小于等于
        /// </summary>
        public const int LessThanOrEqual = 3;

        /// <summary>
        /// 小于
        /// </summary>
        public const int LessThan = 4;

        /// <summary>
        /// 不等于
        /// </summary>
        public const int NotEqual = 5;

        /// <summary>
        /// 在某个范围内
        /// </summary>
        public const int InRange = 6;

        /// <summary>
        /// 不在某个范围内
        /// </summary>
        public const int NotInRange = 7;

        /// <summary>
        /// 包含（用于字符串模糊匹配）
        /// </summary>
        public const int Contains = 8;
    }
}