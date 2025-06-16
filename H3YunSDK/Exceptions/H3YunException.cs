using System;

namespace H3YunSDK.Exceptions
{
    /// <summary>
    /// 氚云API异常基类
    /// </summary>
    public class H3YunException : Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// 初始化氚云API异常
        /// </summary>
        /// <param name="message">错误消息</param>
        public H3YunException(string message) : base(message)
        {
            ErrorCode = "Unknown";
        }

        /// <summary>
        /// 初始化氚云API异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="errorCode">错误代码</param>
        public H3YunException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 初始化氚云API异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        public H3YunException(string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = "Unknown";
        }

        /// <summary>
        /// 初始化氚云API异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="innerException">内部异常</param>
        public H3YunException(string message, string errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}