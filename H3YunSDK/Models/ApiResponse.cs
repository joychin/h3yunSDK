using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace H3YunSDK.Models
{
    /// <summary>
    /// 氚云API响应基类
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// 响应代码
        /// </summary>
        [JsonProperty("Successful")]
        [JsonPropertyName("Successful")]
        public bool Successful { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        [JsonProperty("ErrorMessage")]
        [JsonPropertyName("ErrorMessage")]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 判断响应是否成功
        /// </summary>
        /// <returns>是否成功</returns>
        public bool IsSuccess() => Successful;
    }

    /// <summary>
    /// 氚云API响应泛型类
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// 响应数据
        /// </summary>
        [JsonProperty("ReturnData")]
        [JsonPropertyName("ReturnData")]
        public T? ReturnData { get; set; }
    }
}