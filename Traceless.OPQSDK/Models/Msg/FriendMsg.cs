namespace Traceless.OPQSDK.Models.Msg
{
    /// <summary>
    /// 好友消息
    /// </summary>
    public class FriendMsg : BaseMsg
    {
        /// <summary>
        /// 来自QQ
        /// </summary>
        public long FromUin { get; set; }

        /// <summary>
        /// 目标QQ
        /// </summary>
        public long ToUin { get; set; }
    }
}