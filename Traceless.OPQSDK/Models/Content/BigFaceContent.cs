namespace Traceless.OPQSDK.Models.Content
{
    public class BigFaceContent : BaseContent
    {
        /// <summary>
        /// 大表情 缩略图的Base64编码值
        /// </summary>
        public string ForwordBuf { get; set; }

        /// <summary>
        /// 对应协议所转发的字段
        /// </summary>
        public int ForwordField { get; set; }
    }
}