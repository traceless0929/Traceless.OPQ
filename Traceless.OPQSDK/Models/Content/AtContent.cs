using Newtonsoft.Json;

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
            return this.Tips.Contains("图片") ? GetMsg<PicContent>() : null;
        }

        /// <summary>
        /// 语音信息
        /// </summary>
        /// <returns></returns>
        public VoiceContent GetVoice()
        {
            return this.Tips.Contains("语音") ? GetMsg<VoiceContent>() : null;
        }

        /// <summary>
        /// 大表情信息
        /// </summary>
        /// <returns></returns>
        public BigFaceContent GetBigFace()
        {
            return this.Tips.Contains("大表情") ? GetMsg<BigFaceContent>() : null;
        }
    }
}