using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    public class BanReq
    {
        /// <summary>
        /// 要禁言的群组
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 要禁言的人
        /// </summary>
        public long ShutUpUserID { get; set; }

        /// <summary>
        /// 单位为分钟 0取消禁言
        /// </summary>
        public int ShutTime { get; set; }
    }
}