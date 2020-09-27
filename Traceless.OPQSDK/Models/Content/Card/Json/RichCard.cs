using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Content.Card.Json
{
    public class RichCard
    {
        public RichCard(string title, string desc, string prompt, string tag, string url, string preview)
        {
            this.desc = desc;
            this.prompt = prompt;
            this.meta.news.desc = desc;
            this.meta.news.title = title;
            this.meta.news.tag = tag;
            this.meta.news.jumpUrl = url;
            this.meta.news.preview = preview;
        }

        /// <summary>
        /// appid 默认值，不宜改动
        /// </summary>
        public string app { get; set; } = "com.tencent.structmsg";

        /// <summary>
        /// 描述?
        /// </summary>
        public string desc { get; set; } = "这个描述不知道有啥用";

        /// <summary>
        /// 视图类型，不宜改动
        /// </summary>
        public string view { get; set; } = "news";

        /// <summary>
        /// 版本
        /// </summary>
        public string ver { get; set; } = "0.0.0.1";

        /// <summary>
        /// 在聊天列表里显示的缩略消息
        /// </summary>
        public string prompt { get; set; } = "在聊天列表里显示的缩略消息";

        public Meta meta { get; set; } = new Meta();
    }

    public class Meta
    {
        public News news { get; set; } = new News();
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
}