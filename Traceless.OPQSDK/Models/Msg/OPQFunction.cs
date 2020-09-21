using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Traceless.OPQSDK.Models.Msg
{
    /// <summary>
    /// 表示酷Q消息中内含 [CODE:...] 中的类型
    /// </summary>
    [DefaultValue(OPQFunction.Unknown)]
    public enum OPQFunction
    {
        /// <summary>
        /// 未知类型, 同时也是默认值
        /// </summary>
        [Description("unknown")]
        Unknown,

        /// <summary>
        /// 图片
        /// </summary>
        [Description("pic")]
        Pic,

        /// <summary>
        /// 语音
        /// </summary>
        [Description("voice")]
        Voice
    }
}