using System.Collections.Generic;
using H3YunSDK.Models.BizObjects;
using MatchType = H3YunSDK.Models.BizObjects.MatchType;
using Newtonsoft.Json;
using Xunit;

namespace H3YunSDK.Tests.Models.BizObjects
{
    public class MatcherTests
    {
        [Fact]
        public void Matcher_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var matcher = new Matcher();

            // 验证
            Assert.Equal(MatchType.And, matcher.Type); // 默认值应为And
            Assert.Null(matcher.Conditions);
        }

        [Fact]
        public void Matcher_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var matcher = new Matcher
            {
                Type = MatchType.Or,
                Conditions = new List<ItemMatcher>
                {
                    new ItemMatcher { Field = "F0000001", CompareType = CompareType.Equal, Value = "测试值1" },
                    new ItemMatcher { Field = "F0000002", CompareType = CompareType.Contains, Value = "测试值2" }
                }
            };

            // 验证
            if (matcher != null) {
                Assert.Equal(MatchType.Or, matcher.Type);
            }
            Assert.NotNull(matcher.Conditions);
            Assert.Collection(matcher.Conditions,
                item =>
                {
                    Assert.NotNull(item);
                    Assert.NotNull(item.Field);
                    Assert.Equal("F0000001", item.Field);
                    Assert.Equal(CompareType.Equal, item.CompareType);
                    Assert.NotNull(item.Value);
                    Assert.Equal("测试值1", item.Value);
                },
                item =>
                {
                    Assert.NotNull(item);
                    Assert.NotNull(item.Field);
                    Assert.Equal("F0000002", item.Field);
                    Assert.Equal(CompareType.Contains, item.CompareType);
                    Assert.NotNull(item.Value);
                    Assert.Equal("测试值2", item.Value);
                });
        }

        [Fact]
        public void Matcher_SerializesToJson_Correctly()
        {
            // 准备
            var matcher = new Matcher
            {
                Type = MatchType.Or,
                Conditions = new List<ItemMatcher>
                {
                    new ItemMatcher { Field = "F0000001", CompareType = CompareType.Equal, Value = "测试值1" },
                    new ItemMatcher { Field = "F0000002", CompareType = CompareType.Contains, Value = "测试值2" }
                }
            };

            // 执行
            var json = JsonConvert.SerializeObject(matcher);

            // 验证
            Assert.Contains("\"Type\":\"Or\"", json);
            Assert.Contains("\"Conditions\":[{\"Field\":\"F0000001\",\"CompareType\":\"Equal\",\"Value\":\"测试值1\"},{\"Field\":\"F0000002\",\"CompareType\":\"Contains\",\"Value\":\"测试值2\"}]", json);
        }

        [Fact]
        public void Matcher_DeserializesFromJson_Correctly()
        {
            // 准备
            var json = "{\"Type\":\"Or\",\"Conditions\":[{\"Field\":\"F0000001\",\"CompareType\":\"Equal\",\"Value\":\"测试值1\"},{\"Field\":\"F0000002\",\"CompareType\":\"Contains\",\"Value\":\"测试值2\"}]}";

            // 执行
            var matcher = JsonConvert.DeserializeObject<Matcher>(json);

            // 验证
            Assert.NotNull(matcher);
            Assert.NotNull(matcher!.Conditions);
            var conditions = matcher.Conditions!;
            Assert.Equal(MatchType.Or, matcher.Type);
            Assert.Collection(conditions, 
            item =>
            {
                Assert.NotNull(item);
                Assert.NotNull(item.Field);
                var field = item.Field!;
                Assert.Equal("F0000001", field);
                Assert.Equal(CompareType.Equal, item.CompareType);
                Assert.NotNull(item.Value);
                var value = item.Value!;
                    Assert.Equal("测试值1", value);
                },
                item =>
                {
                    Assert.NotNull(item);
                    var field = item.Field;
                    Assert.NotNull(field);
                    Assert.Equal("F0000002", field);
                    Assert.Equal(CompareType.Contains, item.CompareType);
                    var value = item.Value;
                    Assert.NotNull(value);
                    Assert.Equal("测试值2", value);
                });
            }
        }
    }

    public class ItemMatcherTests
    {
        [Fact]
        public void ItemMatcher_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var itemMatcher = new ItemMatcher();

            // 验证
            Assert.Null(itemMatcher.Field);
            Assert.Equal(CompareType.Equal, itemMatcher.CompareType); // 默认值应为Equal
            Assert.Null(itemMatcher.Value);
        }

        [Fact]
        public void ItemMatcher_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var itemMatcher = new ItemMatcher
            {
                Field = "F0000001",
                CompareType = CompareType.GreaterThan,
                Value = 100
            };

            // 验证
            Assert.Equal("F0000001", itemMatcher.Field);
            Assert.Equal(CompareType.GreaterThan, itemMatcher.CompareType);
            Assert.Equal(100, itemMatcher.Value);
        }

        [Fact]
        public void ItemMatcher_SerializesToJson_Correctly()
        {
            // 准备
            var itemMatcher = new ItemMatcher
            {
                Field = "F0000001",
                CompareType = CompareType.GreaterThan,
                Value = 100
            };

            // 执行
            var json = JsonConvert.SerializeObject(itemMatcher);

            // 验证
            Assert.Contains("\"Field\":\"F0000001\"", json);
            Assert.Contains("\"CompareType\":\"GreaterThan\"", json);
            Assert.Contains("\"Value\":100", json);
        }

        [Fact]
        public void ItemMatcher_DeserializesFromJson_Correctly()
        {
            // 准备
            var json = "{\"Field\":\"F0000001\",\"CompareType\":\"GreaterThan\",\"Value\":100}";

            // 执行
            var itemMatcher = JsonConvert.DeserializeObject<ItemMatcher>(json);

            // 验证
            Assert.NotNull(itemMatcher);
            Assert.Equal("F0000001", itemMatcher.Field);
            Assert.Equal(CompareType.GreaterThan, itemMatcher.CompareType);
            Assert.Equal(100L, itemMatcher.Value); // JSON.NET将数字反序列化为long
        }

        [Theory]
        [InlineData(CompareType.Equal, "Equal")]
        [InlineData(CompareType.NotEqual, "NotEqual")]
        [InlineData(CompareType.GreaterThan, "GreaterThan")]
        [InlineData(CompareType.GreaterThanOrEqual, "GreaterThanOrEqual")]
        [InlineData(CompareType.LessThan, "LessThan")]
        [InlineData(CompareType.LessThanOrEqual, "LessThanOrEqual")]
        [InlineData(CompareType.Contains, "Contains")]
        [InlineData(CompareType.NotContains, "NotContains")]
        [InlineData(CompareType.In, "In")]
        [InlineData(CompareType.NotIn, "NotIn")]
        public void CompareType_SerializesCorrectly(CompareType compareType, string expectedValue)
        {
            // 准备
            var itemMatcher = new ItemMatcher
            {
                Field = "F0000001",
                CompareType = compareType,
                Value = "测试值"
            };

            // 执行
            var json = JsonConvert.SerializeObject(itemMatcher);

            // 验证
            Assert.Contains($"\"CompareType\":\"{expectedValue}\"", json);
        }
    }