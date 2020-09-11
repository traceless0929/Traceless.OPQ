using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 群成员列表请求
    /// </summary>
    public class GroupUserListReq
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long GroupUin { get; set; }

        /// <summary>
        /// 首次为0 当返回体中的LastUin!=0，赋值到这里继续请求
        /// </summary>
        public long LastUin { get; set; } = 0;
    }
}