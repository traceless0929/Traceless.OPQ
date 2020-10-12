namespace Traceless.OPQSDK.Models.Msg
{
    /// <summary>
    /// 消息类型。[G]:支持群消息，[F]:支持好友消息，未注明表示暂未支持解析
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 纯文本消息[FG]，图文混搭时，会丢失图片成为TextMsg
        /// </summary>
        TextMsg,

        /// <summary>
        /// 群成员AT消息[G]，图文混搭时，会成为PicMsg，AtMsg时，Content的子Type需要根据tips判断
        /// </summary>
        AtMsg,

        /// <summary>
        /// 图片消息[FG]
        /// </summary>
        PicMsg,

        /// <summary>
        /// 大表情消息[FG]
        /// </summary>
        BigFaceMsg,

        /// <summary>
        /// 红包消息[F]，群红包为TextMsg
        /// </summary>
        RedBagMsg,

        /// <summary>
        /// 语音消息[FG]
        /// </summary>
        VoiceMsg,

        /// <summary>
        /// Json格式的复杂消息[FG]
        /// </summary>
        JsonMsg,

        /// <summary>
        /// Xml格式的复杂消息[FG]
        /// </summary>
        XmlMsg,

        /// <summary>
        /// 群文件消息[G]
        /// </summary>
        GroupFileMsg,

        /// <summary>
        /// 回复消息[FG]
        /// </summary>
        ReplayMsg,

        /// <summary>
        /// 其他类型的消息[FG]
        /// </summary>
        UnknownMsg
    }
}