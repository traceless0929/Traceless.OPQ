using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 收到加群请求
    /// </summary>
    public class GroupJoinReqArgs
    {
        /// <summary>
        /// 好友QQ
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邀请人ID，非管理员时，该值为0
        /// </summary>
        public long InviteUin { get; set; }
    }
}