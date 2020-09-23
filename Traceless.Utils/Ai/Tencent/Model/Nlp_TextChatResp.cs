using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils.Ai.Tencent.Model
{
    /// <summary>
    /// 智能闲聊&gt;智能闲聊
    /// </summary>
    public class Nlp_TextChatResp
    {
        /// <summary>
        /// 会话标识（应用内唯一）,UTF-8编码，非空且长度上限32字节
        /// </summary>
        public string session { get; set; }

        /// <summary>
        /// 机器人返回数据
        /// </summary>
        public string answer { get; set; }
    }
}