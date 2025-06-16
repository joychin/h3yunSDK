using H3YunSDK.Models;
using Xunit;

namespace H3YunSDK.Tests.Models
{
    public class ApiResponseTests
    {
        [Fact]
        public void ApiResponse_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new ApiResponse();

            // 验证
            Assert.False(response.Successful);
            Assert.Null(response.ErrorMessage);
        }

        [Fact]
        public void ApiResponse_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new ApiResponse
            {
                Successful = true,
                ErrorMessage = "测试错误消息"
            };

            // 验证
            Assert.True(response.Successful);
            Assert.Equal("测试错误消息", response.ErrorMessage);
        }

        [Fact]
        public void IsSuccess_WhenSuccessfulIsTrue_ReturnsTrue()
        {
            // 准备
            var response = new ApiResponse
            {
                Successful = true
            };

            // 执行 & 验证
            Assert.True(response.IsSuccess());
        }

        [Fact]
        public void IsSuccess_WhenSuccessfulIsFalse_ReturnsFalse()
        {
            // 准备
            var response = new ApiResponse
            {
                Successful = false
            };

            // 执行 & 验证
            Assert.False(response.IsSuccess());
        }

        [Fact]
        public void ApiResponseGeneric_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new ApiResponse<TestData>();

            // 验证
            Assert.False(response.Successful);
            Assert.Null(response.ErrorMessage);
            Assert.Null(response.ReturnData);
        }

        [Fact]
        public void ApiResponseGeneric_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var testData = new TestData { Id = 1, Name = "测试数据" };
            var response = new ApiResponse<TestData>
            {
                Successful = true,
                ErrorMessage = "测试错误消息",
                ReturnData = testData
            };

            // 验证
            Assert.True(response.Successful);
            Assert.Equal("测试错误消息", response.ErrorMessage);
            Assert.NotNull(response.ReturnData);
            Assert.Equal(1, response.ReturnData.Id);
            Assert.Equal("测试数据", response.ReturnData.Name);
        }

        [Fact]
        public void ApiResponseGeneric_InheritsFromApiResponse()
        {
            // 准备 & 执行
            var response = new ApiResponse<TestData>();

            // 验证
            Assert.IsAssignableFrom<ApiResponse>(response);
        }

        private class TestData
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
}