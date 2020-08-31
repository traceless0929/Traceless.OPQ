using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// QQ群功能包：加群 拉人 踢群 退群
    /// </summary>
    public class GroupMgrReq
    {
        /// <summary>
        /// 1申请加入群聊 2退出群聊 3移出群聊 8邀请人入群
        /// </summary>
        public int ActionType { get; set; } = 1;

        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 被操作ID【仅ActionType 3,8有效】
        /// </summary>
        public long ActionUserID { get; set; } = 0;

        /// <summary>
        /// 申请理由【仅ActionType 1有效】
        /// </summary>
        public string Content { get; set; } = "";
    }
}