using System;
using System.Collections.Generic;
using System.Text;
using Traceless.Utils.Ai.Tencent.Model;

namespace Traceless.Utils.Ai.Tencent
{
    public static class Apis
    {
        private static string _apiKey = "";
        private static int _appId = 0;

        static Apis()
        {
            _apiKey = System.Configuration.ConfigurationManager.AppSettings["tencent_appkey"];
            _appId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["tencent_appid"]);
        }

        /// <summary>
        /// 获取Appid
        /// </summary>
        /// <returns></returns>
        public static int getAppId()
        {
            return _appId;
        }

        /// <summary>
        /// 获取apiKey
        /// </summary>
        /// <returns></returns>
        public static string getApiKey()
        {
            return _apiKey;
        }

        /// <summary>
        /// 腾讯智能闲聊
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ask"></param>
        /// <returns></returns>
        public static BaseResp<Nlp_TextChatResp> Chat(string session, string ask)
        {
            Nlp_TextChatRequest req = new Nlp_TextChatRequest
            {
                session = session,
                question = ask
            };
            req.sign = Utils.Sign(req, _apiKey);
            return HttpUtils.Get<BaseResp<Nlp_TextChatResp>>(Utils.getChatUrl() + "?" + Utils.Parameter(req));
        }

        /// <summary>
        /// 腾讯语音合成（优图）
        /// </summary>
        /// <param name="text">文字内容</param>
        /// <param name="speaker">普通话男声1 静琪女声5 欢馨女声6 碧萱女声7</param>
        /// <param name="speed">语速，默认为100，取值范围[50, 200]</param>
        /// <param name="format">1-PCM 2-WAV 3-MP3</param>
        /// <returns></returns>
        public static BaseResp<Aai_TtsResp> Tts(string text, int speaker = 7, int speed = 100, int format = 2)
        {
            Aai_TtsRequest req = new Aai_TtsRequest
            {
                text = text,
                speaker = speaker,
                speed = speed,
                format = format
            };
            req.sign = Utils.Sign(req, _apiKey);
            return HttpUtils.Get<BaseResp<Aai_TtsResp>>(Utils.getAaiUrl() + "?" + Utils.Parameter(req));
        }

        /// <summary>
        /// 语义分析
        /// </summary>
        /// <param name="req">需要分析的语句</param>
        /// <returns></returns>
        public static TexSmartResp TexAnalysis(string req)
        {
            return HttpUtils.Post<TexSmartResp>(Utils.getTexUrl(), new TexSmartRequest { str = req });
        }
    }
}