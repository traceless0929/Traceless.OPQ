using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Traceless.Utils.Http;

namespace Traceless.Utils
{
    public static class HttpUtils
    {
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="T">结果类</typeparam>
        /// <param name="url">post地址</param>
        /// <param name="data">请求体</param>
        /// <returns></returns>
        public static T Post<T>(string url, object data, string contentType = "application/json") where T : class
        {
            Task<HttpResponseMessage> responseMessage = PostAsync(url, JsonConvert.SerializeObject(data), contentType);
            if (responseMessage.Result.IsSuccessStatusCode)
            {
                Task<string> t = responseMessage.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(t.Result);
            }
            return default(T);
        }

        /// <summary>
        /// 获取大写编码字符串
        /// </summary>
        private static string GetUpperEncode(string encode)
        {
            var result = new StringBuilder();
            int index = int.MinValue;
            for (int i = 0; i < encode.Length; i++)
            {
                string character = encode[i].ToString();
                if (character == "%")
                    index = i;
                if (i - index == 1 || i - index == 2)
                    character = character.ToUpper();
                result.Append(character);
            }
            return result.ToString();
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <typeparam name="T">结果类</typeparam>
        /// <param name="url">get请求地址</param>
        /// <returns></returns>
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

        private async static Task<HttpResponseMessage> PostAsync(string url, string postStr, string contentType = "application/json")
        {
            ITrHttpClientFactory factory = new TrHttpClientFactory();
            var client = factory.CreateHttpClient();
            return await client.PostAsync(url, postStr, contentType);
        }

        private async static Task<HttpResponseMessage> GetAsync(string url)
        {
            ITrHttpClientFactory factory = new TrHttpClientFactory();
            var client = factory.CreateHttpClient();
            return await client.GetAsync(url);
        }

        /// <summary>
        /// 使用默认编码对 URL 进行编码
        /// </summary>
        /// <param name="url">要编码的地址</param>
        /// <returns>编码后的地址</returns>
		public static string UrlEncode(string url, bool isUpper = false)
        {
            var result = HttpUtility.UrlEncode(url);
            if (!isUpper)
                return result;
            return GetUpperEncode(result);
        }

        /// <summary>
        /// 使用指定的编码 <see cref="Encoding"/> 对 URL 进行编码
        /// </summary>
        /// <param name="url">要编码的地址</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>编码后的地址</returns>
        public static string UrlEncode(string url, Encoding encoding, bool isUpper = false)
        {
            var result = HttpUtility.UrlEncode(url, encoding);
            if (!isUpper)
                return result;
            return GetUpperEncode(result);
        }

        /// <summary>
        /// 使用默认编码对 URL 进行解码
        /// </summary>
        /// <param name="url">要解码的地址</param>
        /// <returns>编码后的地址</returns>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        /// <summary>
        /// 使用指定的编码 <see cref="Encoding"/> 对 URL 进行解码
        /// </summary>
        /// <param name="url">要解码的地址</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>编码后的地址</returns>
        public static string UrlDecode(string url, Encoding encoding)
        {
            return HttpUtility.UrlDecode(url, encoding);
        }

        /// <summary>
        /// MD5加密（小写）
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string MD5(string s, int len = 32)
        {
            string result = "";

            var md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            result = sb.ToString();

            return len == 32 ? result : result.Substring(8, 16);
        }
    }
}