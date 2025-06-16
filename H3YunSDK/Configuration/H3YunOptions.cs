using System;

namespace H3YunSDK.Configuration
{
    /// <summary>
    /// 氚云API配置选项
    /// </summary>
    public class H3YunOptions
    {
        /// <summary>
        /// 氚云API基础URL
        /// </summary>
        public string BaseUrl { get; set; } = "https://www.h3yun.com";

        /// <summary>
        /// 企业引擎编码
        /// </summary>
        public string EngineCode { get; set; } = string.Empty;

        /// <summary>
        /// 企业引擎密钥
        /// </summary>
        public string EngineSecret { get; set; } = string.Empty;

        /// <summary>
        /// API请求超时时间（秒）
        /// </summary>
        public int TimeoutSeconds { get; set; } = 60;

        /// <summary>
        /// 验证配置是否有效
        /// </summary>
        /// <exception cref="ArgumentException">当配置无效时抛出</exception>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(BaseUrl))
            {
                throw new ArgumentException("BaseUrl不能为空", nameof(BaseUrl));
            }

            if (string.IsNullOrWhiteSpace(EngineCode))
            {
                throw new ArgumentException("EngineCode不能为空", nameof(EngineCode));
            }

            if (string.IsNullOrWhiteSpace(EngineSecret))
            {
                throw new ArgumentException("EngineSecret不能为空", nameof(EngineSecret));
            }

            if (TimeoutSeconds <= 0)
            {
                throw new ArgumentException("TimeoutSeconds必须大于0", nameof(TimeoutSeconds));
            }
        }
    }
}