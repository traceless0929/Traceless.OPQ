using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Msg
{
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