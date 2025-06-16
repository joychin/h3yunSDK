using System;
using H3YunSDK.Configuration;
using H3YunSDK.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace H3YunSDK.Extensions
{
    /// <summary>
    /// 服务集合扩展方法
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加氚云SDK服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        /// <param name="sectionName">配置节名称</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddH3YunSdk(
            this IServiceCollection services,
            IConfiguration configuration,
            string sectionName = "H3Yun")
        {
            services.Configure<H3YunOptions>(configuration.GetSection(sectionName));
            services.AddHttpClient<IH3YunClient, H3YunClient>();
            
            return services;
        }

        /// <summary>
        /// 添加氚云SDK服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configureOptions">配置选项</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddH3YunSdk(
            this IServiceCollection services,
            Action<H3YunOptions> configureOptions)
        {
            services.Configure(configureOptions);
            services.AddHttpClient<IH3YunClient, H3YunClient>();
            
            return services;
        }
    }
}