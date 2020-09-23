using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils.Ai.Tencent.Model
{
    /// <summary>
    /// 请求公共参数
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// 【公共参数】应用标识（AppId）
        /// </summary>
        public int app_id { get; set; } = Apis.getAppId();

        /// <summary>
        /// 【公共参数】请求时间戳（秒级）
        /// </summary>
        public long time_stamp { get; set; } = TimeStamp.ConvertToTimeStamp(DateTime.Now) / 1000;

        /// <summary>
        /// 【公共参数】非空且长度上限32字节 随机字符串
        /// </summary>
        public string nonce_str { get; set; } = (TimeStamp.ConvertToTimeStamp(DateTime.Now) + "").Substring(0, 10);

        /// <summary>
        /// 【公共参数】非空且长度固定32字节 签名信息，详见接口鉴权
        /// </summary>
        public string sign { get; set; }
    }
}