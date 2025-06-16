using System.Threading;
using System.Threading.Tasks;
using H3YunSDK.Models;
using H3YunSDK.Models.BizObjects;
using H3YunSDK.Models.Workflow;

namespace H3YunSDK.Interfaces
{
    /// <summary>
    /// 氚云API客户端接口
    /// </summary>
    public interface IH3YunClient
    {
        /// <summary>
        /// 创建业务对象
        /// </summary>
        /// <param name="request">创建业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>创建业务对象响应</returns>
        Task<ApiResponse<CreateBizObjectResponse>> CreateBizObjectAsync(
            CreateBizObjectRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量创建业务对象
        /// </summary>
        /// <param name="request">批量创建业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>批量创建业务对象响应</returns>
        Task<ApiResponse<CreateBizObjectsResponse>> CreateBizObjectsAsync(
            CreateBizObjectsRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 加载业务对象
        /// </summary>
        /// <param name="request">加载业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>加载业务对象响应</returns>
        Task<ApiResponse<LoadBizObjectResponse>> LoadBizObjectAsync(
            LoadBizObjectRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新业务对象
        /// </summary>
        /// <param name="request">更新业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>更新业务对象响应</returns>
        Task<ApiResponse<UpdateBizObjectResponse>> UpdateBizObjectAsync(
            UpdateBizObjectRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除业务对象
        /// </summary>
        /// <param name="request">删除业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>删除业务对象响应</returns>
        Task<ApiResponse<RemoveBizObjectResponse>> RemoveBizObjectAsync(
            RemoveBizObjectRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取业务对象列表
        /// </summary>
        /// <param name="request">获取业务对象列表请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>获取业务对象列表响应</returns>
        Task<ApiResponse<ListBizObjectsResponse>> ListBizObjectsAsync(
            ListBizObjectsRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量加载业务对象
        /// </summary>
        /// <param name="request">批量加载业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>批量加载业务对象响应</returns>
        Task<ApiResponse<LoadBizObjectsResponse>> LoadBizObjectsAsync(
            LoadBizObjectsRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量更新业务对象
        /// </summary>
        /// <param name="request">批量更新业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>批量更新业务对象响应</returns>
        Task<ApiResponse<UpdateBizObjectsResponse>> UpdateBizObjectsAsync(
            UpdateBizObjectsRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量删除业务对象
        /// </summary>
        /// <param name="request">批量删除业务对象请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>批量删除业务对象响应</returns>
        Task<ApiResponse<RemoveBizObjectsResponse>> RemoveBizObjectsAsync(
            RemoveBizObjectsRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取工作流信息
        /// </summary>
        /// <param name="request">获取工作流信息请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>获取工作流信息响应</returns>
        Task<ApiResponse<GetWorkflowInfoResponse>> GetWorkflowInfoAsync(
            GetWorkflowInfoRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交工作流
        /// </summary>
        /// <param name="request">提交工作流请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>提交工作流响应</returns>
        Task<ApiResponse<SubmitWorkflowResponse>> SubmitWorkflowAsync(
            SubmitWorkflowRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="request">上传附件请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>上传附件响应</returns>
        Task<ApiResponse<UploadAttachmentResponse>> UploadAttachmentAsync(
            UploadAttachmentRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 下载业务对象文件
        /// </summary>
        /// <param name="request">下载业务对象文件请求</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>下载业务对象文件响应</returns>
        Task<ApiResponse<DownloadBizObjectFileResponse>> DownloadBizObjectFileAsync(
            DownloadBizObjectFileRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 调用自定义接口
        /// </summary>
        /// <typeparam name="TRequest">请求类型</typeparam>
        /// <typeparam name="TResponse">响应类型</typeparam>
        /// <param name="method">接口方法名</param>
        /// <param name="request">请求数据</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>自定义接口响应</returns>
        Task<ApiResponse<TResponse>> InvokeCustomApiAsync<TRequest, TResponse>(
            string method,
            TRequest request,
            CancellationToken cancellationToken = default);
    }
}