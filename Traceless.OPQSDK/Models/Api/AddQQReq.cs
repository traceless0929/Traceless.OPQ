using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 添加好友请求
    /// </summary>
    public class AddQQReq
    {
        /// <summary>
        /// 目标QQ
        /// </summary>
        public long AddUserUid { get; set; }

        /// <summary>
        /// 来源 为2004 时 请填群ID 其他情况为0
        /// </summary>
        public long FromGroupID { get; set; } = 0;

        /// <summary>
        /// 来源 2011空间 2020QQ搜索 2004群组 2005讨论组
        /// </summary>
        public int AddFromSource { get; set; } = 2020;

        /// <summary>
        /// 添加好友理由
        /// </summary>
        public string Content { get; set; } = "做个朋友呗";
    }
}