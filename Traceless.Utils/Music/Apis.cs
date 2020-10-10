using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Traceless.Utils.Music.Model;

namespace Traceless.Utils.Music
{
    public class Apis
    {
        private const string qmusic = "https://c.y.qq.com/soso/fcgi-bin/client_search_cp?ct=24&qqmusic_ver=1298&new_json=1&remoteplace=txt.yqq.song&searchid=&t=0&aggr=1&cr=1&catZhida=1&lossless=0&flag_qc=0&p=1&n=20&w=";

        public static QQMusicResp QMusic(string keyword)
        {
            Task<HttpResponseMessage> responseMessage = HttpUtils.GetAsync(qmusic + HttpUtils.UrlEncode(keyword));
            if (responseMessage.Result.IsSuccessStatusCode)
            {
                string resp = responseMessage.Result.Content.ReadAsStringAsync().Result;
                if (resp.Contains("no results"))
                {
                    return null;
                }
                resp = resp.Substring(9, resp.Length - 10);
                return JsonConvert.DeserializeObject<QQMusicResp>(resp);
            }
            return null;
        }
    }
}