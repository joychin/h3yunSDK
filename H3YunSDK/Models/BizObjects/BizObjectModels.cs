using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace H3YunSDK.Models.BizObjects
{
    #region 枚举类型

    /// <summary>
    /// 比较类型
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum CompareType
    {
        /// <summary>
        /// 等于
        /// </summary>
        [EnumMember(Value = "Equal")]
        Equal,

        /// <summary>
        /// 不等于
        /// </summary>
        [EnumMember(Value = "NotEqual")]
        NotEqual,

        /// <summary>
        /// 大于
        /// </summary>
        [EnumMember(Value = "GreaterThan")]
        GreaterThan,

        /// <summary>
        /// 大于等于
        /// </summary>
        [EnumMember(Value = "GreaterThanOrEqual")]
        GreaterThanOrEqual,

        /// <summary>
        /// 小于
        /// </summary>
        [EnumMember(Value = "LessThan")]
        LessThan,

        /// <summary>
        /// 小于等于
        /// </summary>
        [EnumMember(Value = "LessThanOrEqual")]
        LessThanOrEqual,

        /// <summary>
        /// 包含
        /// </summary>
        [EnumMember(Value = "Contains")]
        Contains,

        /// <summary>
        /// 不包含
        /// </summary>
        [EnumMember(Value = "NotContains")]
        NotContains,

        /// <summary>
        /// 在...之中
        /// </summary>
        [EnumMember(Value = "In")]
        In,

        /// <summary>
        /// 不在...之中
        /// </summary>
        [EnumMember(Value = "NotIn")]
        NotIn
    }

    /// <summary>
    /// 匹配类型
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum MatchType
    {
        /// <summary>
        /// 与
        /// </summary>
        [EnumMember(Value = "And")]
        And,

        /// <summary>
        /// 或
        /// </summary>
        [EnumMember(Value = "Or")]
        Or
    }

    #endregion

    #region 通用模型

    /// <summary>
    /// 用户对象
    /// </summary>
    public class UserObject
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty("ObjectId")]
        [JsonPropertyName("ObjectId")]
        public string? ObjectId { get; set; }
        
        /// <summary>
        /// 用户名称
        /// </summary>
        [JsonProperty("Name")]
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
    }

    /// <summary>
    /// 部门对象
    /// </summary>
    public class DepartmentObject
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        [JsonProperty("ObjectId")]
        [JsonPropertyName("ObjectId")]
        public string? ObjectId { get; set; }
        
        /// <summary>
        /// 部门名称
        /// </summary>
        [JsonProperty("Name")]
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
    }

    /// <summary>
    /// 业务对象数据
    /// </summary>
    public class BizObject : Dictionary<string, object>
    {
        /// <summary>
        /// 业务对象ID
        /// </summary>
        [JsonProperty("ObjectId")]
        [JsonPropertyName("ObjectId")]
        public string? ObjectId { get; set; }

        /// <summary>
        /// 业务对象名称
        /// </summary>
        [JsonProperty("Name")]
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [JsonProperty("CreatedBy")]
        [JsonPropertyName("CreatedBy")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// 归属人ID
        /// </summary>
        [JsonProperty("OwnerId")]
        [JsonPropertyName("OwnerId")]
        public string? OwnerId { get; set; }

        /// <summary>
        /// 归属部门ID
        /// </summary>
        [JsonProperty("OwnerDeptId")]
        [JsonPropertyName("OwnerDeptId")]
        public string? OwnerDeptId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("CreatedTime")]
        [JsonPropertyName("CreatedTime")]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [JsonProperty("ModifiedBy")]
        [JsonPropertyName("ModifiedBy")]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonProperty("ModifiedTime")]
        [JsonPropertyName("ModifiedTime")]
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [JsonProperty("WorkflowInstanceId")]
        [JsonPropertyName("WorkflowInstanceId")]
        public string? WorkflowInstanceId { get; set; }

        /// <summary>
    /// 数据状态
    /// </summary>
    [JsonProperty("Status")]
    [JsonPropertyName("Status")]
    public int Status { get; set; }

    /// <summary>
    /// 序列号
    /// </summary>
    [JsonProperty("SeqNo")]
    [JsonPropertyName("SeqNo")]
    public string? SeqNo { get; set; }

    /// <summary>
    /// 修改人对象信息
    /// </summary>
    [JsonProperty("ModifiedByObject")]
    [JsonPropertyName("ModifiedByObject")]
    public UserObject? ModifiedByObject { get; set; }

    /// <summary>
    /// 创建人对象信息
    /// </summary>
    [JsonProperty("CreatedByObject")]
    [JsonPropertyName("CreatedByObject")]
    public UserObject? CreatedByObject { get; set; }

    /// <summary>
    /// 归属人对象信息
    /// </summary>
    [JsonProperty("OwnerIdObject")]
    [JsonPropertyName("OwnerIdObject")]
    public UserObject? OwnerIdObject { get; set; }

    /// <summary>
    /// 归属部门对象信息
    /// </summary>
    [JsonProperty("OwnerDeptIdObject")]
    [JsonPropertyName("OwnerDeptIdObject")]
    public DepartmentObject? OwnerDeptIdObject { get; set; }

    /// <summary>
    /// 单选用户对象
    /// </summary>
    [JsonProperty("SingleUserObject")]
    [JsonPropertyName("SingleUserObject")]
    public UserObject? SingleUserObject { get; set; }

    /// <summary>
    /// 多选用户对象数组
    /// </summary>
    [JsonProperty("MultiUserObject")]
    [JsonPropertyName("MultiUserObject")]
    public List<UserObject>? MultiUserObject { get; set; }

    /// <summary>
    /// 单选部门对象
    /// </summary>
    [JsonProperty("SingleDepartmentObject")]
    [JsonPropertyName("SingleDepartmentObject")]
    public DepartmentObject? SingleDepartmentObject { get; set; }

    /// <summary>
    /// 多选部门对象数组
    /// </summary>
    [JsonProperty("MultiDepartmentObject")]
    [JsonPropertyName("MultiDepartmentObject")]
    public List<DepartmentObject>? MultiDepartmentObject { get; set; }

    /// <summary>
    /// 关联单个对象
    /// </summary>
    [JsonProperty("Association")]
    [JsonPropertyName("Association")]
    public string? Association { get; set; }

    /// <summary>
    /// 关联对象数组
    /// </summary>
    [JsonProperty("AssociationArray")]
    [JsonPropertyName("AssociationArray")]
    public List<string>? AssociationArray { get; set; }

    /// <summary>
    /// 签名数组
    /// </summary>
    [JsonProperty("Autograph")]
    [JsonPropertyName("Autograph")]
    public List<string>? Autograph { get; set; }
}

    /// <summary>
    /// 过滤条件
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// 起始行号
        /// </summary>
        [JsonProperty("FromRowNum")]
        [JsonPropertyName("FromRowNum")]
        public int FromRowNum { get; set; } = 0;

        /// <summary>
        /// 结束行号
        /// </summary>
        [JsonProperty("ToRowNum")]
        [JsonPropertyName("ToRowNum")]
        public int ToRowNum { get; set; } = 100;

        /// <summary>
        /// 是否需要计数
        /// </summary>
        [JsonProperty("RequireCount")]
        [JsonPropertyName("RequireCount")]
        public bool RequireCount { get; set; } = true;

        /// <summary>
        /// 返回字段集合
        /// </summary>
        [JsonProperty("ReturnItems")]
        [JsonPropertyName("ReturnItems")]
        public List<string>? ReturnItems { get; set; }

        /// <summary>
        /// 排序集合
        /// </summary>
        [JsonProperty("SortByCollection")]
        [JsonPropertyName("SortByCollection")]
        public List<SortBy>? SortByCollection { get; set; }

        /// <summary>
        /// 匹配器
        /// </summary>
        [JsonProperty("Matcher")]
        [JsonPropertyName("Matcher")]
        public Matcher? Matcher { get; set; }
    }

    /// <summary>
    /// 排序
    /// </summary>
    public class SortBy
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        [JsonProperty("Field")]
        [JsonPropertyName("Field")]
        public string Field { get; set; } = string.Empty;

        /// <summary>
        /// 排序方向
        /// </summary>
        [JsonProperty("Direction")]
        [JsonPropertyName("Direction")]
        public string Direction { get; set; } = "Ascending";
    }

    /// <summary>
    /// 匹配器
    /// </summary>
    public class Matcher
    {
        /// <summary>
        /// 匹配类型
        /// </summary>
        [JsonProperty("Type")]
        [JsonPropertyName("Type")]
        public MatchType Type { get; set; } = MatchType.And;

        /// <summary>
        /// 子匹配器集合
        /// </summary>
        [JsonProperty("Conditions")]
        [JsonPropertyName("Matchers")]
        public List<ItemMatcher>? Conditions { get; set; }
    }

    /// <summary>
    /// 项目匹配器
    /// </summary>
    public class ItemMatcher
    {
        /// <summary>
        /// 字段名
        /// </summary>
        [JsonProperty("Field")]
        [JsonPropertyName("Field")]
        public string? Field { get; set; }

        /// <summary>
        /// 比较类型
        /// </summary>
        [JsonProperty("CompareType")]
        [JsonPropertyName("CompareType")]
        public CompareType CompareType { get; set; } = CompareType.Equal;

        /// <summary>
        /// 值
        /// </summary>
        [JsonProperty("Value")]
        [JsonPropertyName("Value")]
        public object? Value { get; set; }
    }

    #endregion

    #region 创建业务对象

    /// <summary>
    /// 创建业务对象请求
    /// </summary>
    public class CreateBizObjectRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务对象数据
        /// </summary>
        [JsonProperty("BizObject")]
        [JsonPropertyName("BizObject")]
        public BizObject BizObject { get; set; } = new BizObject();

        /// <summary>
        /// 是否提交
        /// </summary>
        [JsonProperty("IsSubmit")]
        [JsonPropertyName("IsSubmit")]
        public bool IsSubmit { get; set; } = false;
    }

    /// <summary>
    /// 创建业务对象响应
    /// </summary>
    public class CreateBizObjectResponse
    {
        /// <summary>
        /// 业务对象ID
        /// </summary>
        [JsonProperty("ObjectId")]
        [JsonPropertyName("ObjectId")]
        public string? ObjectId { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [JsonProperty("WorkflowInstanceId")]
        [JsonPropertyName("WorkflowInstanceId")]
        public string? WorkflowInstanceId { get; set; }
    }

    #endregion

    #region 加载业务对象

    /// <summary>
    /// 加载业务对象请求
    /// </summary>
    public class LoadBizObjectRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务对象ID
        /// </summary>
        [JsonProperty("BizObjectId")]
        [JsonPropertyName("BizObjectId")]
        public string BizObjectId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 加载业务对象响应
    /// </summary>
    public class LoadBizObjectResponse : BizObject
    {
    }

    #endregion

    #region 更新业务对象

    /// <summary>
    /// 更新业务对象请求
    /// </summary>
    public class UpdateBizObjectRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务对象数据
        /// </summary>
        [JsonProperty("BizObject")]
        [JsonPropertyName("BizObject")]
        public BizObject BizObject { get; set; } = new BizObject();

        /// <summary>
        /// 是否提交
        /// </summary>
        [JsonProperty("IsSubmit")]
        [JsonPropertyName("IsSubmit")]
        public bool IsSubmit { get; set; } = false;
    }

    /// <summary>
    /// 更新业务对象响应
    /// </summary>
    public class UpdateBizObjectResponse
    {
        /// <summary>
        /// 业务对象ID
        /// </summary>
        [JsonProperty("ObjectId")]
        [JsonPropertyName("ObjectId")]
        public string? ObjectId { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [JsonProperty("WorkflowInstanceId")]
        [JsonPropertyName("WorkflowInstanceId")]
        public string? WorkflowInstanceId { get; set; }
    }

    #endregion

    #region 删除业务对象

    /// <summary>
    /// 删除业务对象请求
    /// </summary>
    public class RemoveBizObjectRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务对象ID
        /// </summary>
        [JsonProperty("BizObjectId")]
        [JsonPropertyName("BizObjectId")]
        public string BizObjectId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 删除业务对象响应
    /// </summary>
    public class RemoveBizObjectResponse
    {
        /// <summary>
        /// 业务对象ID
        /// </summary>
        [JsonProperty("ObjectId")]
        [JsonPropertyName("ObjectId")]
        public string? ObjectId { get; set; }
    }

    #endregion

    #region 获取业务对象列表

    /// <summary>
    /// 获取业务对象列表请求
    /// </summary>
    public class ListBizObjectsRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 过滤条件
        /// </summary>
        [JsonProperty("Filter")]
        [JsonPropertyName("Filter")]
        public Filter? Filter { get; set; }
    }

    /// <summary>
    /// 获取业务对象列表响应
    /// </summary>
    public class ListBizObjectsResponse
    {
        /// <summary>
        /// 业务对象列表
        /// </summary>
        [JsonProperty("BizObjects")]
        [JsonPropertyName("BizObjects")]
        public List<BizObject>? BizObjects { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        [JsonProperty("TotalCount")]
        [JsonPropertyName("TotalCount")]
        public int TotalCount { get; set; }
    }

    #endregion

    #region 批量创建业务对象

    /// <summary>
    /// 批量创建业务对象请求
    /// </summary>
    public class CreateBizObjectsRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务对象数组（JSON字符串数组）
        /// </summary>
        [JsonProperty("BizObjectArray")]
        [JsonPropertyName("BizObjectArray")]
        public string[] BizObjectArray { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 是否提交
        /// </summary>
        [JsonProperty("IsSubmit")]
        [JsonPropertyName("IsSubmit")]
        public bool IsSubmit { get; set; } = false;
    }

    /// <summary>
    /// 批量创建业务对象响应
    /// </summary>
    public class CreateBizObjectsResponse
    {
        /// <summary>
        /// 创建成功的业务对象ID列表
        /// </summary>
        [JsonProperty("ObjectIds")]
        [JsonPropertyName("ObjectIds")]
        public List<string>? ObjectIds { get; set; }

        /// <summary>
        /// 工作流实例ID列表
        /// </summary>
        [JsonProperty("WorkflowInstanceIds")]
        [JsonPropertyName("WorkflowInstanceIds")]
        public List<string>? WorkflowInstanceIds { get; set; }
    }

    #endregion

    #region 批量加载业务对象

    /// <summary>
    /// 批量加载业务对象请求
    /// </summary>
    public class LoadBizObjectsRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 过滤条件（JSON字符串格式）
        /// </summary>
        [JsonProperty("Filter")]
        [JsonPropertyName("Filter")]
        public string Filter { get; set; } = string.Empty;
    }

    /// <summary>
    /// 批量加载业务对象响应
    /// </summary>
    public class LoadBizObjectsResponse
    {
        /// <summary>
        /// 业务对象数组
        /// </summary>
        [JsonProperty("BizObjectArray")]
        [JsonPropertyName("BizObjectArray")]
        public List<BizObject>? BizObjectArray { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        [JsonProperty("TotalCount")]
        [JsonPropertyName("TotalCount")]
        public int TotalCount { get; set; }
    }

    #endregion

    #region UpdateBizObjects Models

    /// <summary>
    /// 批量更新业务对象请求
    /// </summary>
    public class UpdateBizObjectsRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务对象数组（JSON字符串数组）
        /// </summary>
        [JsonProperty("BizObjectArray")]
        [JsonPropertyName("BizObjectArray")]
        public string[] BizObjectArray { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 业务对象ID数组
        /// </summary>
        [JsonProperty("BizObjectIds")]
        [JsonPropertyName("BizObjectIds")]
        public string[] BizObjectIds { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// 批量更新业务对象响应
    /// </summary>
    public class UpdateBizObjectsResponse
    {
        /// <summary>
        /// 更新成功的对象ID列表
        /// </summary>
        [JsonProperty("ObjectIds")]
        [JsonPropertyName("ObjectIds")]
        public List<string>? ObjectIds { get; set; }
    }

    #endregion

    #region RemoveBizObjects Models

    /// <summary>
    /// 批量删除业务对象请求
    /// </summary>
    public class RemoveBizObjectsRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务对象ID数组
        /// </summary>
        [JsonProperty("BizObjectIds")]
        [JsonPropertyName("BizObjectIds")]
        public string[] BizObjectIds { get; set; } = Array.Empty<string>();
    }

    /// <summary>
    /// 批量删除业务对象响应
    /// </summary>
    public class RemoveBizObjectsResponse
    {
        /// <summary>
        /// 删除成功的对象ID列表
        /// </summary>
        [JsonProperty("ObjectIds")]
        [JsonPropertyName("ObjectIds")]
        public List<string>? ObjectIds { get; set; }
    }

    #endregion

    #region UploadAttachment Models

    /// <summary>
    /// 上传附件请求
    /// </summary>
    public class UploadAttachmentRequest
    {
        /// <summary>
        /// 表单编码
        /// </summary>
        public string SchemaCode { get; set; } = string.Empty;

        /// <summary>
        /// 文件属性名称
        /// </summary>
        public string FilePropertyName { get; set; } = string.Empty;

        /// <summary>
        /// 业务对象ID
        /// </summary>
        public string BizObjectId { get; set; } = string.Empty;

        /// <summary>
        /// 文件字节数组
        /// </summary>
        public byte[] FileBytes { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// 文件名（必须带后缀）
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件内容类型
        /// </summary>
        public string ContentType { get; set; } = string.Empty;
    }

    /// <summary>
    /// 上传附件响应
    /// </summary>
    public class UploadAttachmentResponse
    {
        /// <summary>
        /// 是否上传成功
        /// </summary>
        [JsonProperty("Success")]
        [JsonPropertyName("Success")]
        public bool Success { get; set; }

        /// <summary>
        /// 附件ID
        /// </summary>
        [JsonProperty("AttachmentId")]
        [JsonPropertyName("AttachmentId")]
        public string? AttachmentId { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("ErrorMessage")]
        [JsonPropertyName("ErrorMessage")]
        public string? ErrorMessage { get; set; }
    }

    #endregion

    #region DownloadBizObjectFile Models

    /// <summary>
    /// 下载业务对象文件请求
    /// </summary>
    public class DownloadBizObjectFileRequest
    {
        /// <summary>
        /// 附件ID
        /// </summary>
        public string AttachmentId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 下载业务对象文件响应
    /// </summary>
    public class DownloadBizObjectFileResponse
    {
        /// <summary>
        /// 文件字节数组
        /// </summary>
        public byte[] FileBytes { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件内容类型
        /// </summary>
        public string ContentType { get; set; } = string.Empty;
    }

    #endregion
}