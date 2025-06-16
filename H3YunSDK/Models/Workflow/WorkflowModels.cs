using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace H3YunSDK.Models.Workflow
{
    #region 获取工作流信息

    /// <summary>
    /// 获取工作流信息请求
    /// </summary>
    public class GetWorkflowInfoRequest
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [JsonProperty("InstanceId")]
        [JsonPropertyName("InstanceId")]
        public string InstanceId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 获取工作流信息响应
    /// </summary>
    public class GetWorkflowInfoResponse
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [JsonProperty("InstanceId")]
        [JsonPropertyName("InstanceId")]
        public string? InstanceId { get; set; }

        /// <summary>
        /// 工作流编码
        /// </summary>
        [JsonProperty("WorkflowCode")]
        [JsonPropertyName("WorkflowCode")]
        public string? WorkflowCode { get; set; }

        /// <summary>
        /// 工作流名称
        /// </summary>
        [JsonProperty("WorkflowName")]
        [JsonPropertyName("WorkflowName")]
        public string? WorkflowName { get; set; }

        /// <summary>
        /// 表单编码
        /// </summary>
        [JsonProperty("SchemaCode")]
        [JsonPropertyName("SchemaCode")]
        public string? SchemaCode { get; set; }

        /// <summary>
        /// 业务对象ID
        /// </summary>
        [JsonProperty("BizObjectId")]
        [JsonPropertyName("BizObjectId")]
        public string? BizObjectId { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        [JsonProperty("Originator")]
        [JsonPropertyName("Originator")]
        public string? Originator { get; set; }

        /// <summary>
        /// 发起人名称
        /// </summary>
        [JsonProperty("OriginatorName")]
        [JsonPropertyName("OriginatorName")]
        public string? OriginatorName { get; set; }

        /// <summary>
        /// 发起时间
        /// </summary>
        [JsonProperty("StartTime")]
        [JsonPropertyName("StartTime")]
        public string? StartTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [JsonProperty("FinishTime")]
        [JsonPropertyName("FinishTime")]
        public string? FinishTime { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        [JsonProperty("State")]
        [JsonPropertyName("State")]
        public int State { get; set; }

        /// <summary>
        /// 当前活动节点
        /// </summary>
        [JsonProperty("Activities")]
        [JsonPropertyName("Activities")]
        public List<ActivityInfo>? Activities { get; set; }

        /// <summary>
        /// 审批记录
        /// </summary>
        [JsonProperty("ApprovalLogs")]
        [JsonPropertyName("ApprovalLogs")]
        public List<ApprovalLog>? ApprovalLogs { get; set; }
    }

    /// <summary>
    /// 活动节点信息
    /// </summary>
    public class ActivityInfo
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        [JsonProperty("ActivityId")]
        [JsonPropertyName("ActivityId")]
        public string? ActivityId { get; set; }

        /// <summary>
        /// 活动编码
        /// </summary>
        [JsonProperty("ActivityCode")]
        [JsonPropertyName("ActivityCode")]
        public string? ActivityCode { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [JsonProperty("ActivityName")]
        [JsonPropertyName("ActivityName")]
        public string? ActivityName { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        [JsonProperty("ActivityType")]
        [JsonPropertyName("ActivityType")]
        public string? ActivityType { get; set; }

        /// <summary>
        /// 参与者
        /// </summary>
        [JsonProperty("Participants")]
        [JsonPropertyName("Participants")]
        public List<Participant>? Participants { get; set; }
    }

    /// <summary>
    /// 参与者
    /// </summary>
    public class Participant
    {
        /// <summary>
        /// 参与者ID
        /// </summary>
        [JsonProperty("ParticipantId")]
        [JsonPropertyName("ParticipantId")]
        public string? ParticipantId { get; set; }

        /// <summary>
        /// 参与者名称
        /// </summary>
        [JsonProperty("ParticipantName")]
        [JsonPropertyName("ParticipantName")]
        public string? ParticipantName { get; set; }
    }

    /// <summary>
    /// 审批记录
    /// </summary>
    public class ApprovalLog
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        [JsonProperty("ActivityId")]
        [JsonPropertyName("ActivityId")]
        public string? ActivityId { get; set; }

        /// <summary>
        /// 活动编码
        /// </summary>
        [JsonProperty("ActivityCode")]
        [JsonPropertyName("ActivityCode")]
        public string? ActivityCode { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [JsonProperty("ActivityName")]
        [JsonPropertyName("ActivityName")]
        public string? ActivityName { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        [JsonProperty("ApproverID")]
        [JsonPropertyName("ApproverID")]
        public string? ApproverID { get; set; }

        /// <summary>
        /// 审批人名称
        /// </summary>
        [JsonProperty("ApproverName")]
        [JsonPropertyName("ApproverName")]
        public string? ApproverName { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        [JsonProperty("ApprovalTime")]
        [JsonPropertyName("ApprovalTime")]
        public string? ApprovalTime { get; set; }

        /// <summary>
        /// 审批结果
        /// </summary>
        [JsonProperty("ApprovalResult")]
        [JsonPropertyName("ApprovalResult")]
        public string? ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [JsonProperty("ApprovalComment")]
        [JsonPropertyName("ApprovalComment")]
        public string? ApprovalComment { get; set; }
    }

    #endregion

    #region 提交工作流

    /// <summary>
    /// 提交工作流请求
    /// </summary>
    public class SubmitWorkflowRequest
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [JsonProperty("InstanceId")]
        [JsonPropertyName("InstanceId")]
        public string InstanceId { get; set; } = string.Empty;

        /// <summary>
        /// 审批动作
        /// </summary>
        [JsonProperty("ApprovalAction")]
        [JsonPropertyName("ApprovalAction")]
        public string ApprovalAction { get; set; } = string.Empty;

        /// <summary>
        /// 审批意见
        /// </summary>
        [JsonProperty("Comment")]
        [JsonPropertyName("Comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        [JsonProperty("UserId")]
        [JsonPropertyName("UserId")]
        public string? UserId { get; set; }
    }

    /// <summary>
    /// 提交工作流响应
    /// </summary>
    public class SubmitWorkflowResponse
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [JsonProperty("InstanceId")]
        [JsonPropertyName("InstanceId")]
        public string? InstanceId { get; set; }

        /// <summary>
        /// 业务对象ID
        /// </summary>
        [JsonProperty("BizObjectId")]
        [JsonPropertyName("BizObjectId")]
        public string? BizObjectId { get; set; }
    }

    #endregion
}