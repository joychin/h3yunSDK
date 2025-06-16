using System;
using System.Collections.Generic;
using H3YunSDK.Models.BizObjects;
using Newtonsoft.Json;
using Xunit;

namespace H3YunSDK.Tests.Models.BizObjects
{
    public class BizObjectRequestResponseTests
    {
        #region CreateBizObjectRequest Tests

        [Fact]
        public void CreateBizObjectRequest_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new CreateBizObjectRequest();

            // 验证
            Assert.Equal(string.Empty, request.SchemaCode);
            Assert.NotNull(request.BizObject);
            Assert.False(request.IsSubmit);
        }

        [Fact]
        public void CreateBizObjectRequest_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var bizObject = new BizObject
            {
                Name = "测试业务对象"
            };
            bizObject["F0000001"] = "测试字段1";

            var request = new CreateBizObjectRequest
            {
                SchemaCode = "TestSchema",
                BizObject = bizObject,
                IsSubmit = true
            };

            // 验证
            Assert.Equal("TestSchema", request.SchemaCode);
            Assert.Same(bizObject, request.BizObject);
            Assert.True(request.IsSubmit);
        }

        [Fact]
        public void CreateBizObjectRequest_SerializesToJson_Correctly()
        {
            // 准备
            var bizObject = new BizObject
            {
                Name = "测试业务对象"
            };
            bizObject["F0000001"] = "测试字段1";

            var request = new CreateBizObjectRequest
            {
                SchemaCode = "TestSchema",
                BizObject = bizObject,
                IsSubmit = true
            };

            // 执行
            var json = JsonConvert.SerializeObject(request);

            // 验证
            Assert.Contains("\"SchemaCode\":\"TestSchema\"", json);
            Assert.Contains("\"BizObject\":", json);
            Assert.Contains("\"Name\":\"测试业务对象\"", json);
            Assert.Contains("\"F0000001\":\"测试字段1\"", json);
            Assert.Contains("\"IsSubmit\":true", json);
        }

        #endregion

        #region CreateBizObjectResponse Tests

        [Fact]
        public void CreateBizObjectResponse_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new CreateBizObjectResponse();

            // 验证
            Assert.Null(response.ObjectId);
            Assert.Null(response.WorkflowInstanceId);
        }

        [Fact]
        public void CreateBizObjectResponse_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new CreateBizObjectResponse
            {
                ObjectId = "TestObjectId",
                WorkflowInstanceId = "TestInstanceId"
            };

            // 验证
            Assert.Equal("TestObjectId", response.ObjectId);
            Assert.Equal("TestInstanceId", response.WorkflowInstanceId);
        }

        #endregion

        #region LoadBizObjectRequest Tests

        [Fact]
        public void LoadBizObjectRequest_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new LoadBizObjectRequest();

            // 验证
            Assert.Null(request.SchemaCode);
            Assert.Null(request.BizObjectId);
        }

        [Fact]
        public void LoadBizObjectRequest_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new LoadBizObjectRequest
            {
                SchemaCode = "TestSchema",
                BizObjectId = "TestObjectId"
            };

            // 验证
            Assert.Equal("TestSchema", request.SchemaCode);
            Assert.Equal("TestObjectId", request.BizObjectId);
        }

        #endregion

        #region LoadBizObjectResponse Tests

        [Fact]
        public void LoadBizObjectResponse_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new LoadBizObjectResponse();

            // 验证
            Assert.Null(response.ObjectId);
            Assert.Null(response.Name);
        }

        [Fact]
        public void LoadBizObjectResponse_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new LoadBizObjectResponse
            {
                ObjectId = "TestObjectId",
                Name = "测试业务对象"
            };
            response.Add("F0000001", "测试字段1");

            // 验证
            Assert.Equal("TestObjectId", response.ObjectId);
            Assert.Equal("测试业务对象", response.Name);
            Assert.Equal("测试字段1", response["F0000001"]);
        }

        #endregion

        #region UpdateBizObjectRequest Tests

        [Fact]
        public void UpdateBizObjectRequest_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new UpdateBizObjectRequest();

            // 验证
            Assert.Equal(string.Empty, request.SchemaCode);
            Assert.NotNull(request.BizObject);
            Assert.False(request.IsSubmit);
        }

        [Fact]
        public void UpdateBizObjectRequest_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var bizObject = new BizObject
            {
                ObjectId = "TestObjectId",
                Name = "测试业务对象"
            };
            bizObject["F0000001"] = "测试字段1";

            var request = new UpdateBizObjectRequest
            {
                SchemaCode = "TestSchema",
                BizObject = bizObject,
                IsSubmit = true
            };

            // 验证
            Assert.Equal("TestSchema", request.SchemaCode);
            Assert.Same(bizObject, request.BizObject);
            Assert.True(request.IsSubmit);
        }

        #endregion

        #region UpdateBizObjectResponse Tests

        [Fact]
        public void UpdateBizObjectResponse_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new UpdateBizObjectResponse();

            // 验证
            Assert.Null(response.WorkflowInstanceId);
        }

        [Fact]
        public void UpdateBizObjectResponse_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new UpdateBizObjectResponse
            {
                WorkflowInstanceId = "TestInstanceId"
            };

            // 验证
            Assert.Equal("TestInstanceId", response.WorkflowInstanceId);
        }

        #endregion

        #region RemoveBizObjectRequest Tests

        [Fact]
        public void RemoveBizObjectRequest_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new RemoveBizObjectRequest();

            // 验证
            Assert.Null(request.SchemaCode);
            Assert.Null(request.BizObjectId);
        }

        [Fact]
        public void RemoveBizObjectRequest_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new RemoveBizObjectRequest
            {
                SchemaCode = "TestSchema",
                BizObjectId = "TestObjectId"
            };

            // 验证
            Assert.Equal("TestSchema", request.SchemaCode);
            Assert.Equal("TestObjectId", request.BizObjectId);
        }

        #endregion

        #region RemoveBizObjectResponse Tests

        [Fact]
        public void RemoveBizObjectResponse_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new RemoveBizObjectResponse();

            // 验证
            // 没有属性需要验证
        }

        #endregion

        #region ListBizObjectsRequest Tests

        [Fact]
        public void ListBizObjectsRequest_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new ListBizObjectsRequest();

            // 验证
            Assert.Null(request.SchemaCode);
            Assert.Null(request.Filter);
        }

        [Fact]
        public void ListBizObjectsRequest_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var filter = new Filter
            {
                FromRowNum = 0,
                ToRowNum = 10,
                ReturnItems = new List<string> { "F0000001", "F0000002" }
            };

            var request = new ListBizObjectsRequest
            {
                SchemaCode = "TestSchema",
                Filter = filter
            };

            // 验证
            Assert.Equal("TestSchema", request.SchemaCode);
            Assert.Same(filter, request.Filter);
        }

        #endregion

        #region ListBizObjectsResponse Tests

        [Fact]
        public void ListBizObjectsResponse_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new ListBizObjectsResponse();

            // 验证
            Assert.Equal(0, response.TotalCount);
            Assert.Null(response.BizObjects);
        }

        [Fact]
        public void ListBizObjectsResponse_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var bizObjects = new List<BizObject>
            {
                new BizObject
                {
                    ObjectId = "TestObjectId1",
                    Name = "测试业务对象1",
                    ["F0000001"] = "测试字段1"
                },
                new BizObject
                {
                    ObjectId = "TestObjectId2",
                    Name = "测试业务对象2",
                    ["F0000001"] = "测试字段2"
                }
            };

            var response = new ListBizObjectsResponse
            {
                BizObjects = bizObjects,
                TotalCount = 2
            };

            // 验证
            Assert.Same(bizObjects, response.BizObjects);
            Assert.Equal(2, response.TotalCount);
            Assert.Collection(response.BizObjects,
                item => Assert.NotNull(item),
                item => Assert.NotNull(item));
        }

        #endregion
    }
}