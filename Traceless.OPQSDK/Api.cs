using Newtonsoft.Json;
using Traceless.OPQSDK.Models.Api;
using Traceless.Utils.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Traceless.OPQSDK
{
    public class Api
    {
        private static string _ApiAddress = "";
        private static string _RobotQQ = "";

        static Api()
        {
            _RobotQQ = System.Configuration.ConfigurationManager.AppSettings["robotqq"];
            _ApiAddress = System.Configuration.ConfigurationManager.AppSettings["address"] + "v1/LuaApiCaller?qq=" + _RobotQQ + "&timeout=10";
        }

        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="txt">文字内容</param>
        /// <param name="voice">语音消息【http开头的网络地址，或base64内容】</param>
        /// <param name="pic">图片消息【http开头的网络地址，或base64内容】</param>
        /// <returns></returns>
        public static MsgResp SendGroupMsg(long groupId, string txt = "", string voice = "", string pic = "")
        {
            if (string.IsNullOrEmpty(txt + voice + pic))
            {
                return new MsgResp() { Ret = 1, Msg = "消息为空" };
            }
            SendMsgReq req = new SendMsgReq() { toUser = groupId, sendMsgType = "TextMsg", sendToType = 2, content = (txt == null ? "" : txt) };
            if (!string.IsNullOrEmpty(voice))
            {
                req.content = "";
                req.sendMsgType = "VoiceMsg";
                req.voiceUrl = voice.StartsWith("http") ? voice : "";
                req.voiceBase64Buf = voice.StartsWith("http") ? "" : voice;
            }
            else if (!string.IsNullOrEmpty(pic))
            {
                req.sendMsgType = "PicMsg";
                req.picUrl = pic.StartsWith("http") ? pic : "";
                req.picBase64Buf = pic.StartsWith("http") ? "" : pic;
            }
            return SendMsg(req);
        }

        /// <summary>
        /// 发送好友消息
        /// </summary>
        /// <param name="qq">好友QQ</param>
        /// <param name="txt">文字内容</param>
        /// <param name="voice">语音消息【http开头的网络地址，或base64内容】</param>
        /// <param name="pic">图片消息【http开头的网络地址，或base64内容】</param>
        /// <returns></returns>
        public static MsgResp SendFriendMsg(long qq, string txt = "", string voice = "", string pic = "")
        {
            if (string.IsNullOrEmpty(txt + voice + pic))
            {
                return new MsgResp() { Ret = 1, Msg = "消息为空" };
            }
            SendMsgReq req = new SendMsgReq() { toUser = qq, sendMsgType = "TextMsg", sendToType = 1, content = (txt == null ? "" : txt) };
            if (!string.IsNullOrEmpty(voice))
            {
                req.content = "";
                req.sendMsgType = "VoiceMsg";
                req.voiceUrl = voice.StartsWith("http") ? voice : "";
                req.voiceBase64Buf = voice.StartsWith("http") ? "" : voice;
            }
            else if (!string.IsNullOrEmpty(pic))
            {
                req.sendMsgType = "PicMsg";
                req.picUrl = pic.StartsWith("http") ? pic : "";
                req.picBase64Buf = pic.StartsWith("http") ? "" : pic;
            }
            return SendMsg(req);
        }

        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private static MsgResp SendMsg(SendMsgReq req)
        {
            return Post<MsgResp>(_ApiAddress + "&funcname=SendMsg", req);
        }

        public static T Post<T>(string url, object data) where T : class
        {
            Task<HttpResponseMessage> responseMessage = PostAsync(url, JsonConvert.SerializeObject(data));
            if (responseMessage.Result.IsSuccessStatusCode)
            {
                Task<string> t = responseMessage.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(t.Result);
            }
            return default(T);
        }

        public static T Get<T>(string url) where T : class
        {
            Task<HttpResponseMessage> responseMessage = GetAsync(url);
            if (responseMessage.Result.IsSuccessStatusCode)
            {
                Task<string> t = responseMessage.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(t.Result);
            }
            return default(T);
        }

        private async static Task<HttpResponseMessage> PostAsync(string url, string postStr)
        {
            ITrHttpClientFactory factory = new TrHttpClientFactory();
            var client = factory.CreateHttpClient();
            return await client.PostAsync(url, postStr);
        }

        private async static Task<HttpResponseMessage> GetAsync(string url)
        {
            ITrHttpClientFactory factory = new TrHttpClientFactory();
            var client = factory.CreateHttpClient();
            return await client.GetAsync(url);
        }
    }
}