using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 好友撤回消息
    /// </summary>
    public class FriendRevokeArgs
    {
        /// <summary>
        /// 消息序列号
        /// </summary>
        public long MsgSeq { get; set; }

        /// <summary>
        /// 好友QQ
        /// </summary>
        public long UserID { get; set; }
    }
}