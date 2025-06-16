using System;
using System.Collections.Generic;
using H3YunSDK.Models.Workflow;
using Newtonsoft.Json;
using Xunit;

namespace H3YunSDK.Tests.Models.Workflow
{
    public class WorkflowModelsTests
    {
        #region GetWorkflowInfoRequest Tests

        [Fact]
        public void GetWorkflowInfoRequest_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new GetWorkflowInfoRequest();

            // 验证
            Assert.Null(request.InstanceId);
        }

        [Fact]
        public void GetWorkflowInfoRequest_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new GetWorkflowInfoRequest
            {
                InstanceId = "TestInstanceId"
            };

            // 验证
            Assert.Equal("TestInstanceId", request.InstanceId);
        }

        [Fact]
        public void GetWorkflowInfoRequest_SerializesToJson_Correctly()
        {
            // 准备
            var request = new GetWorkflowInfoRequest
            {
                InstanceId = "TestInstanceId"
            };

            // 执行
            var json = JsonConvert.SerializeObject(request);

            // 验证
            Assert.Contains("\"InstanceId\":\"TestInstanceId\"", json);
        }

        #endregion

        #region GetWorkflowInfoResponse Tests

        [Fact]
        public void GetWorkflowInfoResponse_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new GetWorkflowInfoResponse();

            // 验证
            Assert.Null(response.WorkflowCode);
            Assert.Null(response.WorkflowName);
            Assert.Null(response.SchemaCode);
            Assert.Null(response.BizObjectId);
            Assert.Null(response.Originator);
            Assert.Null(response.OriginatorName);
            Assert.Null(response.StartTime);
            Assert.Null(response.FinishTime);
            Assert.Equal(0, response.State);
            Assert.Null(response.Activities);
            Assert.Null(response.ApprovalLogs);
        }

        [Fact]
        public void GetWorkflowInfoResponse_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var now = DateTime.Now;
            var activityInfos = new List<ActivityInfo>
            {
                new ActivityInfo
                {
                    ActivityCode = "Activity1",
                    ActivityName = "活动1",
                    Participants = new List<Participant>
                    {
                        new Participant { ParticipantId = "User1", ParticipantName = "用户1" }
                    }
                }
            };

            var approvalLogs = new List<ApprovalLog>
            {
                new ApprovalLog
                {
                    ApproverID = "User1",
                    ApproverName = "用户1",
                    ApprovalTime = now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ApprovalResult = "同意",
                    ApprovalComment = "测试评论"
                }
            };

            var response = new GetWorkflowInfoResponse
            {
                WorkflowCode = "TestWorkflow",
                WorkflowName = "测试工作流",
                SchemaCode = "TestSchema",
                BizObjectId = "TestObjectId",
                Originator = "User1",
                OriginatorName = "用户1",
                StartTime = now.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                FinishTime = now.ToString("yyyy-MM-dd HH:mm:ss"),
                State = 1,
                Activities = activityInfos,
                ApprovalLogs = approvalLogs
            };

            // 验证
            Assert.Equal("TestWorkflow", response.WorkflowCode);
            Assert.Equal("测试工作流", response.WorkflowName);
            Assert.Equal("TestSchema", response.SchemaCode);
            Assert.Equal("TestObjectId", response.BizObjectId);
            Assert.Equal("User1", response.Originator);
            Assert.Equal("用户1", response.OriginatorName);
            Assert.Equal(now.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss"), response.StartTime);
            Assert.Equal(now.ToString("yyyy-MM-dd HH:mm:ss"), response.FinishTime);
            Assert.Equal(1, response.State);
            Assert.Same(activityInfos, response.Activities);
            Assert.Same(approvalLogs, response.ApprovalLogs);
        }

        #endregion

        #region ActivityInfo Tests

        [Fact]
        public void ActivityInfo_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var activityInfo = new ActivityInfo();

            // 验证
            Assert.Null(activityInfo.ActivityCode);
            Assert.Null(activityInfo.ActivityName);
            Assert.Null(activityInfo.Participants);
        }

        [Fact]
        public void ActivityInfo_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var participants = new List<Participant>
            {
                new Participant { ParticipantId = "User1", ParticipantName = "用户1" },
                new Participant { ParticipantId = "User2", ParticipantName = "用户2" }
            };

            var activityInfo = new ActivityInfo
            {
                ActivityCode = "Activity1",
                ActivityName = "活动1",
                Participants = participants
            };

            // 验证
            Assert.Equal("Activity1", activityInfo.ActivityCode);
            Assert.Equal("活动1", activityInfo.ActivityName);
            Assert.Same(participants, activityInfo.Participants);
            Assert.Collection(activityInfo.Participants,
                item =>
                {
                    Assert.Equal("User1", item.ParticipantId);
                    Assert.Equal("用户1", item.ParticipantName);
                },
                item =>
                {
                    Assert.Equal("User2", item.ParticipantId);
                    Assert.Equal("用户2", item.ParticipantName);
                });
        }

        #endregion

        #region Participant Tests

        [Fact]
        public void Participant_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var participant = new Participant();

            // 验证
            Assert.Null(participant.ParticipantId);
            Assert.Null(participant.ParticipantName);
        }

        [Fact]
        public void Participant_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var participant = new Participant
            {
                ParticipantId = "User1",
                ParticipantName = "用户1"
            };

            // 验证
            Assert.Equal("User1", participant.ParticipantId);
            Assert.Equal("用户1", participant.ParticipantName);
        }

        #endregion

        #region ApprovalLog Tests

        [Fact]
        public void ApprovalLog_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var approvalLog = new ApprovalLog();

            // 验证
            Assert.Null(approvalLog.ApproverID);
            Assert.Null(approvalLog.ApproverName);
            Assert.Null(approvalLog.ApprovalTime);
            Assert.Null(approvalLog.ApprovalResult);
            Assert.Null(approvalLog.ApprovalComment);
        }

        [Fact]
        public void ApprovalLog_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var now = DateTime.Now;
            var approvalLog = new ApprovalLog
            {
                ApproverID = "User1",
                ApproverName = "用户1",
                ApprovalTime = now.ToString("yyyy-MM-dd HH:mm:ss"),
                ApprovalResult = "同意",
                ApprovalComment = "测试评论"
            };

            // 验证
            Assert.Equal("User1", approvalLog.ApproverID);
            Assert.Equal("用户1", approvalLog.ApproverName);
            Assert.Equal(now.ToString(), approvalLog.ApprovalTime.ToString());
            Assert.Equal("同意", approvalLog.ApprovalResult);
            Assert.Equal("测试评论", approvalLog.ApprovalComment);
        }

        #endregion

        #region SubmitWorkflowRequest Tests

        [Fact]
        public void SubmitWorkflowRequest_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new SubmitWorkflowRequest();

            // 验证
            Assert.Equal(string.Empty, request.InstanceId);
            Assert.Equal(string.Empty, request.ApprovalAction);
            Assert.Null(request.Comment);
            Assert.Null(request.UserId);
        }

        [Fact]
        public void SubmitWorkflowRequest_SetProperties_AreCorrect()
        {
            // 准备 & 执行
            var request = new SubmitWorkflowRequest
            {
                InstanceId = "TestInstanceId",
                ApprovalAction = "Submit",
                Comment = "测试评论",
                UserId = "User1"
            };

            // 验证
            Assert.Equal("TestInstanceId", request.InstanceId);
            Assert.Equal("Submit", request.ApprovalAction);
            Assert.Equal("测试评论", request.Comment);
            Assert.Equal("User1", request.UserId);
        }

        [Fact]
        public void SubmitWorkflowRequest_SerializesToJson_Correctly()
        {
            // 准备
            var request = new SubmitWorkflowRequest
            {
                InstanceId = "TestInstanceId",
                ApprovalAction = "Submit",
                Comment = "测试评论",
                UserId = "User1"
            };

            // 执行
            var json = JsonConvert.SerializeObject(request);

            // 验证
            Assert.Contains("\"InstanceId\":\"TestInstanceId\"", json);
            Assert.Contains("\"ApprovalAction\":\"Submit\"", json);
            Assert.Contains("\"Comment\":\"测试评论\"", json);
            Assert.Contains("\"UserId\":\"User1\"", json);
        }

        #endregion

        #region SubmitWorkflowResponse Tests

        [Fact]
        public void SubmitWorkflowResponse_DefaultProperties_AreCorrect()
        {
            // 准备 & 执行
            var response = new SubmitWorkflowResponse();

            // 验证
            // 没有属性需要验证
        }

        #endregion
    }
}