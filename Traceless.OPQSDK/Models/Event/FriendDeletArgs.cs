namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 机器人删除自己的好友
    /// </summary>
    public class FriendDeletArgs
    {
        /// <summary>
        /// 删除的好友QQ
        /// </summary>
        public long UserID { get; set; }
    }
}