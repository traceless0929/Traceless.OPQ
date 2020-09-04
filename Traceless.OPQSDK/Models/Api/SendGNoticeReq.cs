using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    public class SendGNoticeReq
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 是否置顶 1置顶0不置顶
        /// </summary>
        public int Pinned { get; set; }

        /// <summary>
        /// 是否发送新成员 0不发送 20发送
        /// </summary>
        public int Type { get; set; }
    }
}