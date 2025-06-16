using Microsoft.Extensions.Configuration;
using H3YunSDK.Configuration;

namespace H3YunSDK.Tests
{
    /// <summary>
    /// 测试配置帮助类
    /// </summary>
    public static class TestConfiguration
    {
        private static IConfiguration? _configuration;

        /// <summary>
        /// 获取配置实例
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                    _configuration = builder.Build();
                }
                return _configuration;
            }
        }

        /// <summary>
        /// 获取H3Yun配置选项
        /// </summary>
        /// <returns>H3Yun配置选项</returns>
        public static H3YunOptions GetH3YunOptions()
        {
            var options = new H3YunOptions();
            Configuration.GetSection("H3Yun").Bind(options);
            return options;
        }
    }
}