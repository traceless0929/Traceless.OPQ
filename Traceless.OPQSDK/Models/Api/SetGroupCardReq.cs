using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    public class SetGroupCardReq
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 新名片
        /// </summary>
        public string NewNick { get; set; }
    }
}