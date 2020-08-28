using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Content
{
    public class AtContent : BaseContent
    {
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
            if (this.Tips.Contains("图片"))
            {
                return GetMsg<PicContent>();
            }
            return null;
        }

        /// <summary>
        /// 语音信息
        /// </summary>
        /// <returns></returns>
        public VoiceContent GetVoice()
        {
            if (this.Tips.Contains("语音"))
            {
                return GetMsg<VoiceContent>();
            }
            return null;
        }

        /// <summary>
        /// 大表情信息
        /// </summary>
        /// <returns></returns>
        public BigFaceContent GetBigFace()
        {
            if (this.Tips.Contains("大表情"))
            {
                return GetMsg<BigFaceContent>();
            }
            return null;
        }
    }
}