using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    public class EventMsgBase
    {
        /// <summary>
        /// 来源ID
        /// </summary>
        public long FromUin { get; set; }

        /// <summary>
        /// 目标ID
        /// </summary>
        public long ToUin { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public EventType MsgType { get; set; }

        /// <summary>
        /// 操作序列号
        /// </summary>
        public long MsgSeq { get; set; }

        /// <summary>
        /// 事件的文字描述
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 附带红包信息
        /// </summary>

        public RedPackInfo RedBaginfo { get; set; }
    }
}