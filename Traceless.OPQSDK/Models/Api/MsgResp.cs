using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    public class MsgResp
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 0成功 其它失败 sendMsg：34 跟消息长度似乎无关，可以尝试分段重新发送 sendMsg：110 发送失败，你已被移出该群，请重新加群 sendMsg：120
        /// 机器人被禁言 sendMsg：241 消息发送频率过高，对同一个群或好友，建议发消息的最小间隔控制在1100ms以上 sendMsg：299 超过群发言频率限制
        /// </summary>
        public int Ret { get; set; }
    }
}