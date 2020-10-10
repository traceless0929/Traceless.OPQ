using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traceless.Utils.Music.Model
{
    public class QQMusicResp
    {
        public long code { get; set; }
        public Data data { get; set; }
        public string message { get; set; }

        public List<SongItem> getSongs()
        {
            if (data.song != null && data.song.list != null && data.song.list.Length > 0)
            {
                return data.song.list.ToList();
            }
            return new List<SongItem>();
        }
    }

    public class Data
    {
        public string keyword { get; set; }
        public Song song { get; set; }
    }

    public class Song
    {
        public long curnum { get; set; }
        public long curpage { get; set; }
        public SongItem[] list { get; set; }
        public long totalnum { get; set; }
    }

    public class SongItem
    {
        public Album album { get; set; }
        public string desc { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public Singer[] singer { get; set; }
        public string time_public { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }

        public string getJumpUrl()
        {
            return $"https://i.y.qq.com/v8/playsong.html?songtype=0&songmid={mid}";
        }

        public string getPreView()
        {
            return $"http://y.gtimg.cn/music/photo_new/T002R180x180M000{album.pmid}.jpg";
        }

        public string getSongUrl()
        {
            return $"http://aqqmusic.tc.qq.com/amobile.music.tc.qq.com/C400{mid}.m4a";
        }

        public string getDesc()
        {
            if (singer != null && singer.Length > 0)
            {
                return $"{name}-{string.Join("、", singer.Select(p => p.name).ToArray())}";
            }
            return title;
        }
    }

    public class Album
    {
        public long id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string pmid { get; set; }
        public string subtitle { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
    }

    public class Singer
    {
        public long id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
        public long type { get; set; }
        public long uin { get; set; }
    }
}