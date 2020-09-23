using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils.Ai.Tencent.Model
{
    /// <summary>
    /// 智能闲聊&gt;智能闲聊
    /// </summary>
    public class Nlp_TextChatRequest : BaseRequest
    {
        /// <summary>
        /// 会话标识（应用内唯一）,UTF-8编码，非空且长度上限32字节
        /// </summary>
        public string session { get; set; }

        /// <summary>
        /// 用户输入的聊天内容,UTF-8编码，非空且长度上限300字节
        /// </summary>
        public string question { get; set; }
    }
}