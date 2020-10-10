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
            this.meta.news = new News();
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

        public BaseMeta meta { get; set; } = new BaseMeta();
    }
}