using System.Collections.Generic;
using H3YunSDK.Models.BizObjects;
using Newtonsoft.Json;
using Xunit;
using MatchType = H3YunSDK.Models.BizObjects.MatchType;

namespace H3YunSDK.Tests.Models.BizObjects
{
    public class FilterTests
    {
        [Fact]
        public void Filter_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var filter = new Filter();

            // 验证
            Assert.Equal(0, filter.FromRowNum);
            Assert.Equal(0, filter.ToRowNum);
            Assert.Null(filter.ReturnItems);
            Assert.Null(filter.SortByCollection);
            Assert.Null(filter.Matcher);
        }

        [Fact]
        public void Filter_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var filter = new Filter
            {
                FromRowNum = 10,
                ToRowNum = 20,
                ReturnItems = new List<string> { "F0000001", "F0000002" },
                SortByCollection = new List<SortBy> { new SortBy { Field = "F0000001", Direction = "Desc" } },
                Matcher = new Matcher
                {
                    Type = MatchType.And,
                    Conditions = new List<ItemMatcher>
                    {
                        new ItemMatcher { Field = "F0000001", CompareType = CompareType.Equal, Value = "测试值" }
                    }
                }
            };

            // 验证
            Assert.Equal(10, filter.FromRowNum);
            Assert.Equal(20, filter.ToRowNum);
            
            Assert.NotNull(filter.ReturnItems);
            Assert.Collection(filter.ReturnItems,
                item => Assert.Equal("F0000001", item),
                item => Assert.Equal("F0000002", item));
            
            Assert.NotNull(filter.SortByCollection);
            Assert.Single(filter.SortByCollection);
            var sortBy = filter.SortByCollection[0];
            Assert.Equal("F0000001", sortBy.Field);
            Assert.Equal("Desc", sortBy.Direction);
            
            Assert.NotNull(filter.Matcher);
            var matcher = filter.Matcher;
            Assert.NotNull(matcher);
            Assert.Equal(MatchType.And, matcher.Type);
            
            Assert.NotNull(matcher.Conditions);
            Assert.Single(matcher.Conditions);
            var condition = matcher.Conditions[0];
            Assert.NotNull(condition);
            Assert.NotNull(condition.Field);
            Assert.Equal("F0000001", condition.Field);
            Assert.Equal(CompareType.Equal, condition.CompareType);
            Assert.NotNull(condition.Value);
            Assert.Equal("测试值", condition.Value);
        }

        [Fact]
        public void Filter_SerializesToJson_Correctly()
        {
            // 准备
            var filter = new Filter
            {
                FromRowNum = 10,
                ToRowNum = 20,
                ReturnItems = new List<string> { "F0000001", "F0000002" },
                SortByCollection = new List<SortBy> { new SortBy { Field = "F0000001", Direction = "Desc" } },
                Matcher = new Matcher
                {
                    Type = MatchType.And,
                    Conditions = new List<ItemMatcher>
                    {
                        new ItemMatcher { Field = "F0000001", CompareType = CompareType.Equal, Value = "测试值" }
                    }
                }
            };

            // 执行
            var json = JsonConvert.SerializeObject(filter);

            // 验证
            Assert.Contains("\"FromRowNum\":10", json);
            Assert.Contains("\"ToRowNum\":20", json);
            Assert.Contains("\"ReturnItems\":[\"F0000001\",\"F0000002\"]", json);
            Assert.Contains("\"SortByCollection\":[{\"Field\":\"F0000001\",\"Direction\":\"Desc\"}]", json);
            Assert.Contains("\"Matcher\":{\"Type\":\"And\",\"Conditions\":[{\"Field\":\"F0000001\",\"CompareType\":\"Equal\",\"Value\":\"测试值\"}]}", json);
        }

        [Fact]
        public void Filter_DeserializesFromJson_Correctly()
        {
            // 准备
            var json = "{\"FromRowNum\":10,\"ToRowNum\":20,\"ReturnItems\":[\"F0000001\",\"F0000002\"],\"SortByCollection\":[{\"Field\":\"F0000001\",\"Direction\":\"Desc\"}],\"Matcher\":{\"Type\":\"And\",\"Conditions\":[{\"Field\":\"F0000001\",\"CompareType\":\"Equal\",\"Value\":\"测试值\"}]}}";

            // 执行
            var filter = JsonConvert.DeserializeObject<Filter>(json);

            // 验证
            Assert.NotNull(filter);
            Assert.Equal(10, filter.FromRowNum);
            Assert.Equal(20, filter.ToRowNum);
            
            Assert.NotNull(filter.ReturnItems);
            Assert.Collection(filter.ReturnItems,
                item => Assert.Equal("F0000001", item),
                item => Assert.Equal("F0000002", item));
            
            Assert.NotNull(filter.SortByCollection);
            Assert.Single(filter.SortByCollection);
            var sortBy = filter.SortByCollection[0];
            Assert.Equal("F0000001", sortBy.Field);
            Assert.Equal("Desc", sortBy.Direction);
            
            Assert.NotNull(filter.Matcher);
            var matcher = filter.Matcher!;
            Assert.Equal(MatchType.And, matcher.Type);
            
            Assert.NotNull(matcher.Conditions);
            var conditions = matcher.Conditions!;
            Assert.Single(conditions);
            
            Assert.NotNull(conditions[0]);
            var condition = conditions[0]!;
            
            Assert.NotNull(condition.Field);
            var field = condition.Field!;
            Assert.Equal("F0000001", field);
            
            Assert.Equal(CompareType.Equal, condition.CompareType);
            
            Assert.NotNull(condition.Value);
            var value = condition.Value!;
            Assert.Equal("测试值", value);
        }
    }
}