using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Traceless.OPQSDK.Models.Msg
{
    public class GroupMsg : BaseMsg
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long FromGroupId { get; set; }

        /// <summary>
        /// 群名
        /// </summary>
        public string FromGroupName { get; set; }

        /// <summary>
        /// 发送人QQ
        /// </summary>
        public long FromUserId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string FromNickName { get; set; }

        /// <summary>
        /// 发消息时间时间戳
        /// </summary>
        public long MsgTime { get; set; }

        /// <summary>
        /// 随机值
        /// </summary>
        public long MsgRandom { get; set; }
    }
}