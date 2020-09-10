using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    public class SendMsgReq
    {
        /// <summary>
        /// 欲发给的对象 群或QQ好友或私聊对象
        /// </summary>
        public long toUser { get; set; }

        /// <summary>
        /// --发送消息对象的类型 1好友 2群 3私聊
        /// </summary>
        public int sendToType { get; set; }

        /// <summary>
        /// 欲发送消息的类型 "TextMsg","JsonMsg","XmlMsg","ReplayMsg" ,"TeXiaoTextMsg","PicMsg","VoiceMsg","PhoneMsg"
        /// </summary>
        public string sendMsgType { get; set; }

        /// <summary>
        /// 发送的文本内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 发送私聊消息时 在此传入群ID，表示来源群。 其他情况为0
        /// </summary>
        public int groupid { get; set; }

        /// <summary>
        /// 语音网络地址
        /// </summary>
        public string voiceUrl { get; set; }

        /// <summary>
        /// 发本地送语音的buf 转 bas64 编码
        /// </summary>
        public string voiceBase64Buf { get; set; }

        /// <summary>
        /// 发送图片的网络地址
        /// </summary>
        public string picUrl { get; set; }

        /// <summary>
        /// 发本地送语音的buf 转 bas64 编码
        /// </summary>
        public string picBase64Buf { get; set; }

        public string fileMd5 { get; set; } = "";
    }
}