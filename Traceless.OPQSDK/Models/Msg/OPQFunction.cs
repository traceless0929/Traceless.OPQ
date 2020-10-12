using System.ComponentModel;

namespace Traceless.OPQSDK.Models.Msg
{
    /// <summary>
    /// 表示OPQ消息中内含 [CODE:...] 中的类型
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
        Voice,

        /// <summary>
        /// 富文本分享卡片
        /// </summary>
        [Description("rich")]
        Rich
    }
}