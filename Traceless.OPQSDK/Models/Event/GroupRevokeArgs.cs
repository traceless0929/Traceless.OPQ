using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 群撤回消息
    /// </summary>
    public class GroupRevokeArgs
    {
        /// <summary>
        /// 消息序列号
        /// </summary>
        public long MsgSeq { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }
    }
}