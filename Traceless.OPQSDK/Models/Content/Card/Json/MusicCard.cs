using NStandard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Content.Card.Json
{
    public class MusicCard
    {
        public MusicCard(string title, string desc, string songurl, string jumpurl, string previewUrl, string prompt = "")
        {
            this.meta.music = new Music();
            this.meta.music.desc = desc;
            this.meta.music.title = title;
            this.meta.music.jumpUrl = jumpurl;
            this.meta.music.musicUrl = songurl;
            this.meta.music.preview = previewUrl;
            if (prompt.IsNullOrEmpty())
            {
                this.prompt = "[点歌]" + title;
            }
            else
            {
                this.prompt = prompt;
            }
        }

        public string app { get; set; } = "com.tencent.structmsg";
        public string desc { get; set; } = "音乐";
        public string view { get; set; } = "music";
        public string ver { get; set; } = "0.0.0.1";
        public string prompt { get; set; } = "[分享]歌名-歌手";
        public BaseMeta meta { get; set; } = new BaseMeta();
        public Config config { get; set; } = new Config();
    }
}