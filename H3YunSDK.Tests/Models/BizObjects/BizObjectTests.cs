using System;
using System.Collections.Generic;
using H3YunSDK.Models.BizObjects;
using Newtonsoft.Json;
using Xunit;

namespace H3YunSDK.Tests.Models.BizObjects
{
    public class BizObjectTests
    {
        [Fact]
        public void BizObject_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var bizObject = new BizObject();

            // 验证
            Assert.Null(bizObject.ObjectId);
            Assert.Null(bizObject.Name);
            Assert.Null(bizObject.CreatedBy);
            Assert.Null(bizObject.OwnerId);
            Assert.Null(bizObject.OwnerDeptId);
            Assert.Null(bizObject.CreatedTime);
            Assert.Null(bizObject.ModifiedBy);
            Assert.Null(bizObject.ModifiedTime);
            Assert.Null(bizObject.WorkflowInstanceId);
            Assert.Equal(0, bizObject.Status);
        }

        [Fact]
        public void BizObject_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var now = DateTime.Now;
            var bizObject = new BizObject
            {
                ObjectId = "TestObjectId",
                Name = "测试业务对象",
                CreatedBy = "TestUserId",
                OwnerId = "TestOwnerId",
                OwnerDeptId = "TestDeptId",
                CreatedTime = now,
                ModifiedBy = "TestModifierId",
                ModifiedTime = now.AddHours(1),
                WorkflowInstanceId = "TestInstanceId",
                Status = 1
            };

            // 验证
            if (bizObject != null) {
                Assert.Equal("TestObjectId", bizObject.ObjectId);
            }
            if (bizObject != null) {
                Assert.Equal("测试业务对象", bizObject.Name);
            }
            Assert.Equal("TestUserId", bizObject.CreatedBy);
            Assert.Equal("TestOwnerId", bizObject.OwnerId);
            Assert.Equal("TestDeptId", bizObject.OwnerDeptId);
            Assert.NotNull(bizObject.CreatedTime);
            Assert.Equal(now.ToString("yyyy-MM-dd HH:mm:ss"), bizObject.CreatedTime?.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal("TestModifierId", bizObject.ModifiedBy);
            Assert.NotNull(bizObject.ModifiedTime);
            Assert.Equal(now.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss"), bizObject.ModifiedTime?.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal("TestInstanceId", bizObject.WorkflowInstanceId);
            Assert.Equal(1, bizObject.Status);
        }

        [Fact]
        public void BizObject_InheritsFromDictionary()
        {
            // 准备 & 执行
            var bizObject = new BizObject();

            // 验证
            Assert.IsAssignableFrom<Dictionary<string, object>>(bizObject);
        }

        [Fact]
        public void BizObject_CanAddCustomFields()
        {
            // 准备 & 执行
            var bizObject = new BizObject
            {
                ["F0000001"] = "测试字段1",
                ["F0000002"] = 100,
                ["F0000003"] = true,
                ["F0000004"] = DateTime.Now
            };

            // 验证
            Assert.Collection(bizObject.Keys,
                key => Assert.Equal("F0000001", key),
                key => Assert.Equal("F0000002", key),
                key => Assert.Equal("F0000003", key),
                key => Assert.Equal("F0000004", key));
            Assert.Equal("测试字段1", bizObject["F0000001"]);
            Assert.Equal(100, bizObject["F0000002"]);
            Assert.True((bool)bizObject["F0000003"]);
            Assert.IsType<DateTime>(bizObject["F0000004"]);
        }

        [Fact]
        public void BizObject_SerializesToJson_Correctly()
        {
            // 准备
            var now = DateTime.Now;
            var bizObject = new BizObject
            {
                ObjectId = "TestObjectId",
                Name = "测试业务对象",
                CreatedTime = now
            };
            bizObject["F0000001"] = "测试字段1";
            bizObject["F0000002"] = 100;

            // 执行
            var json = JsonConvert.SerializeObject(bizObject);

            // 验证
            Assert.Contains("\"ObjectId\":\"TestObjectId\"", json);
            Assert.Contains("\"Name\":\"测试业务对象\"", json);
            Assert.Contains("\"F0000001\":\"测试字段1\"", json);
            Assert.Contains("\"F0000002\":100", json);
        }

        [Fact]
        public void BizObject_DeserializesFromJson_Correctly()
        {
            // 准备
            var json = "{\"ObjectId\":\"TestObjectId\",\"Name\":\"测试业务对象\",\"F0000001\":\"测试字段1\",\"F0000002\":100}";

            // 执行
            var bizObject = JsonConvert.DeserializeObject<BizObject>(json);

            // 验证
            Assert.NotNull(bizObject);
            var bizObj = bizObject!;
            Assert.Equal("TestObjectId", bizObj.ObjectId);
            Assert.Equal("测试业务对象", bizObj.Name);
            var field1Value = bizObj["F0000001"];
            var field2Value = bizObj["F0000002"];
            Assert.NotNull(field1Value);
            Assert.NotNull(field2Value);
            Assert.Equal("测试字段1", field1Value);
            Assert.Equal(100, Convert.ToInt32(field2Value)); // 转换为int以匹配API预期类型
        }
    }
}