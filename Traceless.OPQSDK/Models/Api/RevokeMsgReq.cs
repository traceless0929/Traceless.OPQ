using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 撤回消息
    /// </summary>
    public class RevokeMsgReq
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 群消息中的MsgSeq
        /// </summary>
        public long MsgSeq { get; set; }

        /// <summary>
        /// 群消息中的MsgRandom
        /// </summary>
        public long MsgRandom { get; set; }
    }
}