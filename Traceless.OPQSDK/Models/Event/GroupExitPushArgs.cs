using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 有人退群
    /// </summary>
    public class GroupExitPushArgs
    {
        /// <summary>
        /// 退群的QQ
        /// </summary>
        public long UserID { get; set; }
    }
}