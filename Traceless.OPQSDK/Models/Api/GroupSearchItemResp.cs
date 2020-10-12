namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 搜索群组返回
    /// </summary>
    public class GroupSearchItemResp
    {
        /// <summary>
        /// 群数据，似乎是群描述的部分内容截取
        /// </summary>
        public string GroupData { get; set; }

        /// <summary>
        /// 群描述
        /// </summary>
        public string GroupDes { get; set; }

        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 群容量
        /// </summary>
        public long GroupMaxMembers { get; set; }

        /// <summary>
        /// 群名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 群公告
        /// </summary>
        public string GroupNotice { get; set; }

        /// <summary>
        /// 群主
        /// </summary>
        public long GroupOwner { get; set; }

        /// <summary>
        /// 群问题
        /// </summary>
        public string GroupQuestion { get; set; }

        /// <summary>
        /// 群员总数
        /// </summary>
        public long GroupTotalMembers { get; set; }
    }
}