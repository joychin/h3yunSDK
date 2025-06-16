using H3YunSDK.Models.BizObjects;
using Newtonsoft.Json;
using Xunit;

namespace H3YunSDK.Tests.Models.BizObjects
{
    public class SortByTests
    {
        [Fact]
        public void SortBy_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var sortBy = new SortBy();

            // 验证
            Assert.Null(sortBy.Field);
            Assert.Null(sortBy.Direction);
        }

        [Fact]
        public void SortBy_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var sortBy = new SortBy
            {
                Field = "F0000001",
                Direction = "Asc"
            };

            // 验证
            if (sortBy != null) {
                Assert.Equal("F0000001", sortBy.Field);
            }
            if (sortBy != null) {
                Assert.Equal("Asc", sortBy.Direction);
            }
        }

        [Fact]
        public void SortBy_SerializesToJson_Correctly()
        {
            // 准备
            var sortBy = new SortBy
            {
                Field = "F0000001",
                Direction = "Asc"
            };

            // 执行
            var json = JsonConvert.SerializeObject(sortBy);

            // 验证
            Assert.Contains("\"Field\":\"F0000001\"", json);
            Assert.Contains("\"Direction\":\"Asc\"", json);
        }

        [Fact]
        public void SortBy_DeserializesFromJson_Correctly()
        {
            // 准备
            var json = "{\"Field\":\"F0000001\",\"Direction\":\"Asc\"}";

            // 执行
            var sortBy = JsonConvert.DeserializeObject<SortBy>(json);

            // 验证
            Assert.NotNull(sortBy);
            Assert.Equal("F0000001", sortBy.Field);
            Assert.Equal("Asc", sortBy.Direction);
        }

        [Theory]
        [InlineData("Asc")]
        [InlineData("Desc")]
        [InlineData("asc")]
        [InlineData("desc")]
        [InlineData("ASC")]
        [InlineData("DESC")]
        public void SortBy_SupportsVariousCaseDirections(string direction)
        {
            // 准备 & 执行
            var sortBy = new SortBy
            {
                Field = "F0000001",
                Direction = direction
            };

            // 验证
            Assert.Equal(direction, sortBy.Direction);
        }
    }
}