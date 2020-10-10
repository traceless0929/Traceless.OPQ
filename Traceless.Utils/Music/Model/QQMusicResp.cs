using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traceless.Utils.Music.Model
{
    public class QQMusicResp
    {
        public int code { get; set; }
        public Data data { get; set; }
        public string message { get; set; }
        public string notice { get; set; }
        public int subcode { get; set; }
        public int time { get; set; }
        public string tips { get; set; }

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
        public int priority { get; set; }
        public object[] qc { get; set; }
        public Semantic semantic { get; set; }
        public Song song { get; set; }
        public int tab { get; set; }
        public object[] taglist { get; set; }
        public int totaltime { get; set; }
        public Zhida zhida { get; set; }
    }

    public class Semantic
    {
        public int curnum { get; set; }
        public int curpage { get; set; }
        public object[] list { get; set; }
        public int totalnum { get; set; }
    }

    public class Song
    {
        public int curnum { get; set; }
        public int curpage { get; set; }
        public SongItem[] list { get; set; }
        public int totalnum { get; set; }
    }

    public class SongItem
    {
        public Action action { get; set; }
        public Album album { get; set; }
        public int chinesesinger { get; set; }
        public string desc { get; set; }
        public string desc_hilight { get; set; }
        public string docid { get; set; }
        public string es { get; set; }
        public File file { get; set; }
        public int fnote { get; set; }
        public int genre { get; set; }
        public Grp[] grp { get; set; }
        public int id { get; set; }
        public int index_album { get; set; }
        public int index_cd { get; set; }
        public int interval { get; set; }
        public int isonly { get; set; }
        public Ksong ksong { get; set; }
        public int language { get; set; }
        public string lyric { get; set; }
        public string lyric_hilight { get; set; }
        public string mid { get; set; }
        public Mv mv { get; set; }
        public string name { get; set; }
        public int newStatus { get; set; }
        public long nt { get; set; }
        public int ov { get; set; }
        public Pay pay { get; set; }
        public int pure { get; set; }
        public int sa { get; set; }
        public Singer1[] singer { get; set; }
        public int status { get; set; }
        public string subtitle { get; set; }
        public int t { get; set; }
        public int tag { get; set; }
        public int tid { get; set; }
        public string time_public { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public int ver { get; set; }
        public Volume volume { get; set; }

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

    public class Action
    {
        public int alert { get; set; }
        public int icons { get; set; }
        public int msg { get; set; }
        public int _switch { get; set; }
    }

    public class Album
    {
        public int id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string pmid { get; set; }
        public string subtitle { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
    }

    public class File
    {
        public int b_30s { get; set; }
        public int e_30s { get; set; }
        public string media_mid { get; set; }
        public int size_128 { get; set; }
        public int size_128mp3 { get; set; }
        public int size_320 { get; set; }
        public int size_320mp3 { get; set; }
        public int size_aac { get; set; }
        public int size_ape { get; set; }
        public int size_dts { get; set; }
        public int size_flac { get; set; }
        public int size_ogg { get; set; }
        public int size_try { get; set; }
        public string strMediaMid { get; set; }
        public int try_begin { get; set; }
        public int try_end { get; set; }
    }

    public class Ksong
    {
        public int id { get; set; }
        public string mid { get; set; }
    }

    public class Mv
    {
        public int id { get; set; }
        public string vid { get; set; }
    }

    public class Pay
    {
        public int pay_down { get; set; }
        public int pay_month { get; set; }
        public int pay_play { get; set; }
        public int pay_status { get; set; }
        public int price_album { get; set; }
        public int price_track { get; set; }
        public int time_free { get; set; }
    }

    public class Volume
    {
        public float gain { get; set; }
        public float lra { get; set; }
        public float peak { get; set; }
    }

    public class Grp
    {
        public Action1 action { get; set; }
        public Album1 album { get; set; }
        public int chinesesinger { get; set; }
        public string desc { get; set; }
        public string desc_hilight { get; set; }
        public string docid { get; set; }
        public string es { get; set; }
        public File1 file { get; set; }
        public int fnote { get; set; }
        public int genre { get; set; }
        public int id { get; set; }
        public int index_album { get; set; }
        public int index_cd { get; set; }
        public int interval { get; set; }
        public int isonly { get; set; }
        public Ksong1 ksong { get; set; }
        public int language { get; set; }
        public string lyric { get; set; }
        public string lyric_hilight { get; set; }
        public string mid { get; set; }
        public Mv1 mv { get; set; }
        public string name { get; set; }
        public int newStatus { get; set; }
        public long nt { get; set; }
        public int ov { get; set; }
        public Pay1 pay { get; set; }
        public int pure { get; set; }
        public int sa { get; set; }
        public Singer[] singer { get; set; }
        public int status { get; set; }
        public string subtitle { get; set; }
        public int t { get; set; }
        public int tag { get; set; }
        public int tid { get; set; }
        public string time_public { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public int ver { get; set; }
        public Volume1 volume { get; set; }
    }

    public class Action1
    {
        public int alert { get; set; }
        public int icons { get; set; }
        public int msg { get; set; }
        public int _switch { get; set; }
    }

    public class Album1
    {
        public int id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string pmid { get; set; }
        public string subtitle { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
    }

    public class File1
    {
        public int b_30s { get; set; }
        public int e_30s { get; set; }
        public string media_mid { get; set; }
        public int size_128 { get; set; }
        public int size_128mp3 { get; set; }
        public int size_320 { get; set; }
        public int size_320mp3 { get; set; }
        public int size_aac { get; set; }
        public int size_ape { get; set; }
        public int size_dts { get; set; }
        public int size_flac { get; set; }
        public int size_ogg { get; set; }
        public int size_try { get; set; }
        public string strMediaMid { get; set; }
        public int try_begin { get; set; }
        public int try_end { get; set; }
    }

    public class Ksong1
    {
        public int id { get; set; }
        public string mid { get; set; }
    }

    public class Mv1
    {
        public int id { get; set; }
        public string vid { get; set; }
    }

    public class Pay1
    {
        public int pay_down { get; set; }
        public int pay_month { get; set; }
        public int pay_play { get; set; }
        public int pay_status { get; set; }
        public int price_album { get; set; }
        public int price_track { get; set; }
        public int time_free { get; set; }
    }

    public class Volume1
    {
        public float gain { get; set; }
        public float lra { get; set; }
        public float peak { get; set; }
    }

    public class Singer
    {
        public int id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
        public int type { get; set; }
        public int uin { get; set; }
    }

    public class Singer1
    {
        public int id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
        public int type { get; set; }
        public int uin { get; set; }
    }

    public class Zhida
    {
        public int type { get; set; }
        public Zhida_Singer zhida_singer { get; set; }
    }

    public class Zhida_Singer
    {
        public int albumNum { get; set; }
        public Hotalbum[] hotalbum { get; set; }
        public Hotsong[] hotsong { get; set; }
        public int mvNum { get; set; }
        public int singerID { get; set; }
        public string singerMID { get; set; }
        public string singerName { get; set; }
        public string singerPic { get; set; }
        public string singername_hilight { get; set; }
        public int songNum { get; set; }
    }

    public class Hotalbum
    {
        public int albumID { get; set; }
        public string albumMID { get; set; }
        public string albumName { get; set; }
        public string albumname_hilight { get; set; }
    }

    public class Hotsong
    {
        public string f { get; set; }
        public int songID { get; set; }
        public string songMID { get; set; }
        public string songName { get; set; }
        public string songname_hilight { get; set; }
    }
}