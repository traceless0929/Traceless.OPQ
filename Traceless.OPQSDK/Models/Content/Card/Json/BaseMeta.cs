using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Content.Card.Json
{
    public class BaseMeta
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public News news { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Music music { get; set; } = null;
    }

    public class News
    {
        /// <summary>
        /// 动作?
        /// </summary>
        public string action { get; set; } = "";

        /// <summary>
        /// 安卓包名?
        /// </summary>
        public string android_pkg_name { get; set; } = "";

        /// <summary>
        /// APP类型?
        /// </summary>
        public int app_type { get; set; } = 1;

        /// <summary>
        /// appid,就这样别改吧
        /// </summary>
        public int appid { get; set; } = 1101031180;

        /// <summary>
        /// 卡片下方的描述
        /// </summary>
        public string desc { get; set; } = "这里是卡片下面的描述，不宜太长";

        /// <summary>
        /// 跳转URL
        /// </summary>
        public string jumpUrl { get; set; } = @"https://traceless.site/";

        /// <summary>
        /// 缩略图
        /// </summary>
        public string preview { get; set; } = @"https://traceless.site/favicon.ico";

        /// <summary>
        /// 来源icon
        /// </summary>
        public string source_icon { get; set; } = "";

        /// <summary>
        /// 来源地址
        /// </summary>
        public string source_url { get; set; } = "";

        /// <summary>
        /// 左下角的标记
        /// </summary>
        public string tag { get; set; } = "左下角的标记";

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; } = "标题";
    }

    public class Music
    {
        /// <summary>
        /// 动作
        /// </summary>
        public string action { get; set; } = "";

        /// <summary>
        /// 安卓包名
        /// </summary>
        public string android_pkg_name { get; set; } = "";

        /// <summary>
        /// app类型
        /// </summary>
        public int app_type { get; set; } = 1;

        /// <summary>
        /// appid
        /// </summary>
        public int appid { get; set; } = 100497308;

        public string desc { get; set; } = "描述";

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string jumpUrl { get; set; } = "";

        /// <summary>
        /// 音频地址
        /// </summary>
        public string musicUrl { get; set; } = "";

        /// <summary>
        /// 封面图
        /// </summary>
        public string preview { get; set; } = "";

        /// <summary>
        /// 来源消息ID
        /// </summary>
        public string sourceMsgId { get; set; } = "0";

        public string source_icon { get; set; } = "";
        public string source_url { get; set; } = "";
        public string tag { get; set; } = "QQ音乐";
        public string title { get; set; } = "标题";
    }

    public class Config
    {
        public bool autosize { get; set; } = true;
        public long ctime { get; set; } = 0;
        public bool forward { get; set; } = true;
        public string token { get; set; } = "4826e6790057fc7a4fcbe5ac71475fce";
        public string type { get; set; } = "normal";
    }
}