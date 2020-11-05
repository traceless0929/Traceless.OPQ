namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 有人退群
    /// from GroupInviteArgs
    /// type 5
    /// </summary>
    public class C_GroupExitArgs
    {
        /// <summary>
        /// 事件类型 固定->有人退群 5
        /// </summary>
        public int Type { get; set; } = 5;
        /// <summary>
        /// 事件描述
        /// </summary>
        public string MsgTypeStr { get; } = "退群消息";
        /// <summary>
        /// 退群的人
        /// </summary>
        public long Who { get; set; }

        /// <summary>
        /// 退群人名称
        /// </summary>
        public string WhoName { get; set; }
        
        /// <summary>
        /// 群ID
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// 群名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 是否主动退群，false时，表示被人踢出
        /// </summary>
        public bool Initiative => ActionUin == 0;

        /// <summary>
        ///  执行人QQ，Initiative为false时有效
        /// </summary>
        public int ActionUin { get; set; }

        /// <summary>
        /// 执行人QQ昵称，Initiative为false时有效
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 执行人群名片，Initiative为false时有效
        /// </summary>
        public string ActionGroupCard { get; set; }
        
    }
}