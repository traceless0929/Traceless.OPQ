using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Traceless.Utils.Ai.Tencent
{
    public class Utils
    {
        private const string rootUrl = @"https://api.ai.qq.com/";
        private const string aai_tts = @"fcgi-bin/aai/aai_tts";
        private const string textchat = @"fcgi-bin/nlp/nlp_textchat";

        /// <summary>
        /// 获取AI语音合成请求地址
        /// </summary>
        /// <returns></returns>
        public static string getAaiUrl()
        {
            return rootUrl + aai_tts;
        }

        /// <summary>
        /// 获取AI闲聊请求地址
        /// </summary>
        /// <returns></returns>
        public static string getChatUrl()
        {
            return rootUrl + textchat;
        }

        /// <summary>
        /// 获取腾讯AI签名
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="rawDic"></param>
        /// <returns></returns>
        public static string Sign(Dictionary<string, string> rawDic, string appkey, string charset = "utf-8")
        {
            var dic = rawDic.OrderBy(x => x.Key);
            var pair = "";
            foreach (var kv in dic)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    pair += kv.Key + "=" + HttpUtils.UrlEncode(kv.Value, Encoding.GetEncoding(charset), true) + "&";
                }
            }
            pair += "app_key=" + appkey;
            var sign = HttpUtils.MD5(pair).ToUpper();
            return sign;
        }

        /// <summary>
        /// 获取腾讯AI签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="appkey"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string Sign<T>(T model, string appkey, string charset = "utf-8") where T : class, new()
        {
            var m = model.GetType();
            var pis = m.GetProperties();
            var pair = new Dictionary<string, string>();
            foreach (var pi in pis)
            {
                if (pi.Name.ToLower() != "sign")
                {
                    object val = pi.GetValue(model, null);
                    pair.Add(pi.Name, val?.ToString() ?? "");
                }
            }

            var sign = Sign(pair, appkey, charset);

            return sign;
        }

        public static string Parameter<T>(T entity, string charset = "utf-8") where T : class, new()
        {
            string result = string.Empty;
            var pis = entity.GetType().GetProperties();
            foreach (var pi in pis)
            {
                string value = pi.GetValue(entity, null)?.ToString();
                if (value != null)
                {
                    result += "&" + pi.Name + "=" + value;
                }
            }
            return result.TrimStart('&');
        }
    }
}