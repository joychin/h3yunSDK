using System;
using H3YunSDK.Exceptions;
using Xunit;

namespace H3YunSDK.Tests.Exceptions
{
    public class H3YunExceptionTests
    {
        [Fact]
        public void Constructor_WithMessage_SetsMessageAndDefaultErrorCode()
        {
            // 准备 & 执行
            var message = "测试异常消息";
            var exception = new H3YunException(message);

            // 验证
            Assert.Equal(message, exception.Message);
            Assert.Equal("Unknown", exception.ErrorCode);
        }

        [Fact]
        public void Constructor_WithMessageAndErrorCode_SetsMessageAndErrorCode()
        {
            // 准备 & 执行
            var message = "测试异常消息";
            var errorCode = "E001";
            var exception = new H3YunException(message, errorCode);

            // 验证
            Assert.Equal(message, exception.Message);
            Assert.Equal(errorCode, exception.ErrorCode);
        }

        [Fact]
        public void Constructor_WithMessageAndInnerException_SetsMessageAndInnerExceptionAndDefaultErrorCode()
        {
            // 准备 & 执行
            var message = "测试异常消息";
            var innerException = new Exception("内部异常");
            var exception = new H3YunException(message, innerException);

            // 验证
            Assert.Equal(message, exception.Message);
            Assert.Equal("Unknown", exception.ErrorCode);
            Assert.Same(innerException, exception.InnerException);
        }

        [Fact]
        public void Constructor_WithMessageAndErrorCodeAndInnerException_SetsAllProperties()
        {
            // 准备 & 执行
            var message = "测试异常消息";
            var errorCode = "E001";
            var innerException = new Exception("内部异常");
            var exception = new H3YunException(message, errorCode, innerException);

            // 验证
            Assert.Equal(message, exception.Message);
            Assert.Equal(errorCode, exception.ErrorCode);
            Assert.Same(innerException, exception.InnerException);
        }
    }
}