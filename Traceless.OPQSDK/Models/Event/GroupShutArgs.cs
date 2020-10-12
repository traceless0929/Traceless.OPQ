namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 群禁言事件
    /// </summary>
    public class GroupShutArgs
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 禁言时间,全体禁言时【0解除 &gt;0开启】
        /// </summary>
        public long ShutTime { get; set; }

        /// <summary>
        /// 被禁言ID【0=全体】
        /// </summary>
        public long UserID { get; set; }
    }
}