using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 群头衔变更
    /// </summary>
    public class GroupTitleChangeArgs
    {
        /// <summary>
        /// 文字描述（恭喜Kar98k获得群主授予的test头衔）
        /// </summary>
        public string Content { get; set; }

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