using System;
using H3YunSDK.Configuration;
using Xunit;

namespace H3YunSDK.Tests
{
    public class H3YunOptionsTests
    {
        [Fact]
        public void Validate_WithValidOptions_DoesNotThrowException()
        {
            // 准备
            var options = new H3YunOptions
            {
                BaseUrl = "https://www.h3yun.com",
                EngineCode = "TestEngineCode",
                EngineSecret = "TestEngineSecret",
                TimeoutSeconds = 60
            };

            // 执行 & 验证
            var exception = Record.Exception(() => options.Validate());
            Assert.Null(exception);
        }

        [Fact]
        public void Validate_WithEmptyBaseUrl_ThrowsArgumentException()
        {
            // 准备
            var options = new H3YunOptions
            {
                BaseUrl = "",
                EngineCode = "TestEngineCode",
                EngineSecret = "TestEngineSecret",
                TimeoutSeconds = 60
            };

            // 执行 & 验证
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Equal("BaseUrl", exception.ParamName);
            Assert.Contains("BaseUrl不能为空", exception.Message);
        }

        [Fact]
        public void Validate_WithEmptyEngineCode_ThrowsArgumentException()
        {
            // 准备
            var options = new H3YunOptions
            {
                BaseUrl = "https://www.h3yun.com",
                EngineCode = "",
                EngineSecret = "TestEngineSecret",
                TimeoutSeconds = 60
            };

            // 执行 & 验证
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Equal("EngineCode", exception.ParamName);
            Assert.Contains("EngineCode不能为空", exception.Message);
        }

        [Fact]
        public void Validate_WithEmptyEngineSecret_ThrowsArgumentException()
        {
            // 准备
            var options = new H3YunOptions
            {
                BaseUrl = "https://www.h3yun.com",
                EngineCode = "TestEngineCode",
                EngineSecret = "",
                TimeoutSeconds = 60
            };

            // 执行 & 验证
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Equal("EngineSecret", exception.ParamName);
            Assert.Contains("EngineSecret不能为空", exception.Message);
        }

        [Fact]
        public void Validate_WithInvalidTimeoutSeconds_ThrowsArgumentException()
        {
            // 准备
            var options = new H3YunOptions
            {
                BaseUrl = "https://www.h3yun.com",
                EngineCode = "TestEngineCode",
                EngineSecret = "TestEngineSecret",
                TimeoutSeconds = 0
            };

            // 执行 & 验证
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Equal("TimeoutSeconds", exception.ParamName);
            Assert.Contains("TimeoutSeconds必须大于0", exception.Message);
        }

        [Fact]
        public void Validate_WithNegativeTimeoutSeconds_ThrowsArgumentException()
        {
            // 准备
            var options = new H3YunOptions
            {
                BaseUrl = "https://www.h3yun.com",
                EngineCode = "TestEngineCode",
                EngineSecret = "TestEngineSecret",
                TimeoutSeconds = -1
            };

            // 执行 & 验证
            var exception = Assert.Throws<ArgumentException>(() => options.Validate());
            Assert.Equal("TimeoutSeconds", exception.ParamName);
            Assert.Contains("TimeoutSeconds必须大于0", exception.Message);
        }
    }
}