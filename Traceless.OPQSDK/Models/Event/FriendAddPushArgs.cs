namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 加好友成功后的通知回馈
    /// </summary>
    public class FriendAddPushArgs
    {
        /// <summary>
        /// 好友QQ
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}