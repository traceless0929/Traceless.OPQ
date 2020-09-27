using Newtonsoft.Json;
using Traceless.OPQSDK.Models.Content;
using Traceless.OPQSDK.Models.Msg;
using System;
using System.Collections.Generic;
using System.Text;
using Traceless.Utils;
using Newtonsoft.Json.Linq;

namespace Traceless.OPQSDK.Models
{
    /// <summary>
    /// 基础消息体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseData<T> where T : class
    {
        /// <summary>
        /// 当前数据包
        /// </summary>
        public CurrentPacket<T> CurrentPacket { get; set; }

        /// <summary>
        /// 当前QQ
        /// </summary>
        public long CurrentQQ { get; set; }
    }

    /// <summary>
    /// 基础包体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CurrentPacket<T> where T : class
    {
        /// <summary>
        /// </summary>
        public string WebConnId { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// 基础消息体
    /// </summary>
    public class BaseMsg
    {
        /// <summary>
        /// 内容 包含各种类型需要根据具体情况判断
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType { get; set; }

        /// <summary>
        /// 红包信息仅当msgtype为红包类型才有
        /// </summary>
        public RedPackInfo RedBaginfo { get; set; }

        /// <summary>
        /// 消息序列号
        /// </summary>
        public long MsgSeq { get; set; }

        private T GetMsg<T>()
        {
            return JsonConvert.DeserializeObject<T>(this.Content);
        }

        /// <summary>
        /// 图片信息
        /// </summary>
        /// <returns></returns>
        public PicContent GetPic()
        {
            if (MsgType.PicMsg == this.MsgType)
            {
                return GetMsg<PicContent>();
            }
            return new PicContent();
        }

        /// <summary>
        /// xml信息
        /// </summary>
        /// <returns></returns>
        public string GetXml()
        {
            if (MsgType.XmlMsg == this.MsgType)
            {
                this.Content = HttpUtils.DeUnicode(this.Content);
                this.Content = JsonConvert.DeserializeObject<BaseContent>(this.Content).Content;
                return this.Content;
            }
            return null;
        }

        /// <summary>
        /// json信息
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            if (MsgType.JsonMsg == this.MsgType)
            {
                this.Content = HttpUtils.DeUnicode(this.Content);
                this.Content = JsonConvert.DeserializeObject<BaseContent>(this.Content).Content;
                //int index = this.Content.LastIndexOf("{\"app\":");
                //if (index >= 0)
                //{
                //    this.Content = this.Content.Substring(index, this.Content.Length - index);
                //}

                return this.Content;
            }
            return null;
        }

        /// <summary>
        /// 语音信息
        /// </summary>
        /// <returns></returns>
        public VoiceContent GetVoice()
        {
            if (MsgType.VoiceMsg == this.MsgType)
            {
                return GetMsg<VoiceContent>();
            }
            return new VoiceContent();
        }

        /// <summary>
        /// 大表情信息
        /// </summary>
        /// <returns></returns>
        public BigFaceContent GetBigFace()
        {
            if (MsgType.BigFaceMsg == this.MsgType)
            {
                return GetMsg<BigFaceContent>();
            }
            return new BigFaceContent();
        }

        /// <summary>
        /// AT信息【只要UserID不为空其实就是AT，如果不想获取AT内容信息，无需获取】
        /// </summary>
        /// <returns></returns>
        public AtContent GetAt()
        {
            if (MsgType.AtMsg == this.MsgType)
            {
                return GetMsg<AtContent>();
            }
            return new AtContent();
        }

        /// <summary>
        /// 回复信息
        /// </summary>
        /// <returns></returns>
        public RepContent GetRep()
        {
            if (MsgType.ReplayMsg == this.MsgType)
            {
                return GetMsg<RepContent>();
            }
            return new RepContent();
        }
    }
}