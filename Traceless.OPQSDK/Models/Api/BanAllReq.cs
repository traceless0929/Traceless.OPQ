using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 全体禁言请求类
    /// </summary>
    public class BanAllReq
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 0关闭全群禁言 15开启全群禁言
        /// </summary>
        public int Switch { get; set; }
    }
}