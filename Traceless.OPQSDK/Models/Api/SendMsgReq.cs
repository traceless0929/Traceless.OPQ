using System.Collections.Generic;

namespace Traceless.OPQSDK.Models.Api
{
    public class SendMsgReq
    {
        /// <summary>
        /// 欲发给的对象 群或QQ好友或私聊对象
        /// </summary>
        public long ToUserUid { get; set; }

        /// <summary>
        /// --发送消息对象的类型 1好友 2群 3私聊
        /// </summary>
        public int SendToType { get; set; }

        /// <summary>
        /// 欲发送消息的类型 "TextMsg","JsonMsg","XmlMsg","ReplayMsg 回复消息" ,"TeXiaoTextMsg 特效文字","PicMsg
        /// 图片消息","VoiceMsg 语音消息","PhoneMsg 发送给设备","ForwordMsg 转发消息"
        /// </summary>
        public string SendMsgType { get; set; }

        /// <summary>
        /// 发送的文本内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送私聊消息时 在此传入群ID，表示来源群。 其他情况为0
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 语音网络地址
        /// </summary>
        public string VoiceUrl { get; set; }

        /// <summary>
        /// 发送语音的本地地址
        /// </summary>
        public string VoicePath { get; set; }

        /// <summary>
        /// 发送图片的网络地址
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 发送图片的本地地址
        /// </summary>
        public string PicPath { get; set; }

        /// <summary>
        /// 是否闪照
        /// </summary>
        public bool FlashPic { get; set; } = false;

        /// <summary>
        /// base64编码后的消息体 一般由框架自动生成
        /// </summary>
        public string ForwordBuf { get; set; }

        /// <summary>
        /// 一般由框架自动生成
        /// </summary>
        public int ForwordField { get; set; }

        /// <summary>
        /// 回复消息才有的，表示要回复的消息
        /// </summary>
        public ReplayInfo ReplayInfo { get; set; }

        /// <summary>
        /// 群多图MD5S图片转发
        /// </summary>
        public List<string> PicMd5s { get; set; } = new List<string>();
    }
}