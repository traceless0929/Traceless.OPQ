using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    public class GroupJoinSucArgs
    {
        /// <summary>
        /// 群名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public long GroupOwner { get; set; }

        /// <summary>
        /// 处理人昵称
        /// </summary>
        public string OwnerName { get; set; }
    }
}