using System;
using System.Collections.Generic;
using H3YunSDK.Configuration;
using H3YunSDK.Extensions;
using H3YunSDK.Http;
using H3YunSDK.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace H3YunSDK.Tests.Extensions
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddH3YunSdk_WithConfigurationSection_RegistersServices()
        {
            // 准备
            var services = new ServiceCollection();
            var configurationSectionMock = new Mock<IConfigurationSection>();
            configurationSectionMock.Setup(x => x.Path).Returns("H3Yun");
            configurationSectionMock.Setup(x => x.Key).Returns("H3Yun");
            configurationSectionMock.Setup(x => x.GetSection("BaseUrl")).Returns(new MockConfigurationSection("https://example.com"));
            configurationSectionMock.Setup(x => x.GetSection("EngineCode")).Returns(new MockConfigurationSection("TestEngineCode"));
            configurationSectionMock.Setup(x => x.GetSection("EngineSecret")).Returns(new MockConfigurationSection("TestEngineSecret"));
            configurationSectionMock.Setup(x => x.GetSection("TimeoutSeconds")).Returns(new MockConfigurationSection("30"));

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x.GetSection("H3Yun")).Returns(configurationSectionMock.Object);

            // 执行
            services.AddH3YunSdk(configurationMock.Object);

            // 验证
            var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<H3YunOptions>>().Value;
            var client = serviceProvider.GetRequiredService<IH3YunClient>();
            var httpClient = serviceProvider.GetRequiredService<H3YunHttpClient>();

            Assert.Equal("https://example.com", options.BaseUrl);
            Assert.Equal("TestEngineCode", options.EngineCode);
            Assert.Equal("TestEngineSecret", options.EngineSecret);
            Assert.Equal(30, options.TimeoutSeconds);
            Assert.NotNull(client);
            Assert.NotNull(httpClient);
        }

        [Fact]
        public void AddH3YunSdk_WithOptionsAction_RegistersServices()
        {
            // 准备
            var services = new ServiceCollection();

            // 执行
            services.AddH3YunSdk(options =>
            {
                options.BaseUrl = "https://example.com";
                options.EngineCode = "TestEngineCode";
                options.EngineSecret = "TestEngineSecret";
                options.TimeoutSeconds = 30;
            });

            // 验证
            var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<H3YunOptions>>().Value;
            var client = serviceProvider.GetRequiredService<IH3YunClient>();
            var httpClient = serviceProvider.GetRequiredService<H3YunHttpClient>();

            Assert.Equal("https://example.com", options.BaseUrl);
            Assert.Equal("TestEngineCode", options.EngineCode);
            Assert.Equal("TestEngineSecret", options.EngineSecret);
            Assert.Equal(30, options.TimeoutSeconds);
            Assert.NotNull(client);
            Assert.NotNull(httpClient);
        }

        [Fact]
        public void AddH3YunSdk_WithInvalidOptions_ThrowsException()
        {
            // 准备
            var services = new ServiceCollection();

            // 执行 & 验证
            services.AddH3YunSdk(options =>
            {
                // 不设置必要的选项
            });

            var serviceProvider = services.BuildServiceProvider();

            // 验证在解析服务时抛出异常
            Assert.Throws<InvalidOperationException>(() => serviceProvider.GetRequiredService<IH3YunClient>());
        }
    }

    // 用于模拟IConfigurationSection的辅助类
    public class MockConfigurationSection : IConfigurationSection
    {
        private readonly string _value;

        public MockConfigurationSection(string value)
        {
            _value = value;
        }

        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Key => throw new NotImplementedException();
        public string Path => throw new NotImplementedException();
        public string Value { get => _value; set => throw new NotImplementedException(); }
        public IEnumerable<IConfigurationSection> GetChildren() => throw new NotImplementedException();
        public IChangeToken GetReloadToken() => throw new NotImplementedException();
        public IConfigurationSection GetSection(string key) => throw new NotImplementedException();
    }
}