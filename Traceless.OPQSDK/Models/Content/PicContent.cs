using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Content
{
    public class PicContent : BaseContent
    {
        /// <summary>
        /// 消息中的图片(PicMsg)
        /// </summary>
        public List<Grouppic> GroupPic { get; set; } = new List<Grouppic>();

        /// <summary>
        /// 消息中的图片(PicMsg)
        /// </summary>
        public List<Grouppic> FriendPic { get; set; } = new List<Grouppic>();
    }

    /// <summary>
    /// 消息中所携带的图片或表情
    /// </summary>
    public class Grouppic
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public long FileId { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string FileMd5 { get; set; }

        /// <summary>
        /// 文件尺寸
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 缩略图的Base64编码值
        /// </summary>
        public string ForwordBuf { get; set; }

        /// <summary>
        /// 对应协议所转发的字段
        /// </summary>

        public int ForwordField { get; set; }

        /// <summary>
        /// 原图URL
        /// </summary>
        public string Url { get; set; }
    }
}