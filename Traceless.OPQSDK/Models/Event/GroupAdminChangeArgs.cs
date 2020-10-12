namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 群管理变更-机器人是不是管理员都能收到此群管变更事件
    /// </summary>
    public class GroupAdminChangeArgs
    {
        /// <summary>
        /// 1升管理 0降管理
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
    }
}