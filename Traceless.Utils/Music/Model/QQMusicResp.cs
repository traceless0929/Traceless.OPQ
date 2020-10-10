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
        public string notice { get; set; }
        public long subcode { get; set; }
        public long time { get; set; }
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
        public long priority { get; set; }
        public object[] qc { get; set; }
        public Semantic semantic { get; set; }
        public Song song { get; set; }
        public long tab { get; set; }
        public object[] taglist { get; set; }
        public long totaltime { get; set; }
        public Zhida zhida { get; set; }
    }

    public class Semantic
    {
        public long curnum { get; set; }
        public long curpage { get; set; }
        public object[] list { get; set; }
        public long totalnum { get; set; }
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
        public Action action { get; set; }
        public Album album { get; set; }
        public long chinesesinger { get; set; }
        public string desc { get; set; }
        public string desc_hilight { get; set; }
        public string docid { get; set; }
        public string es { get; set; }
        public File file { get; set; }
        public long fnote { get; set; }
        public long genre { get; set; }
        public Grp[] grp { get; set; }
        public long id { get; set; }
        public long index_album { get; set; }
        public long index_cd { get; set; }
        public long interval { get; set; }
        public long isonly { get; set; }
        public Ksong ksong { get; set; }
        public long language { get; set; }
        public string lyric { get; set; }
        public string lyric_hilight { get; set; }
        public string mid { get; set; }
        public Mv mv { get; set; }
        public string name { get; set; }
        public long newStatus { get; set; }
        public long nt { get; set; }
        public long ov { get; set; }
        public Pay pay { get; set; }
        public long pure { get; set; }
        public long sa { get; set; }
        public Singer1[] singer { get; set; }
        public long status { get; set; }
        public string subtitle { get; set; }
        public long t { get; set; }
        public long tag { get; set; }
        public long tid { get; set; }
        public string time_public { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
        public long type { get; set; }
        public string url { get; set; }
        public long ver { get; set; }
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
        public long alert { get; set; }
        public long icons { get; set; }
        public long msg { get; set; }
        public long _switch { get; set; }
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

    public class File
    {
        public long b_30s { get; set; }
        public long e_30s { get; set; }
        public string media_mid { get; set; }
        public long size_128 { get; set; }
        public long size_128mp3 { get; set; }
        public long size_320 { get; set; }
        public long size_320mp3 { get; set; }
        public long size_aac { get; set; }
        public long size_ape { get; set; }
        public long size_dts { get; set; }
        public long size_flac { get; set; }
        public long size_ogg { get; set; }
        public long size_try { get; set; }
        public string strMediaMid { get; set; }
        public long try_begin { get; set; }
        public long try_end { get; set; }
    }

    public class Ksong
    {
        public long id { get; set; }
        public string mid { get; set; }
    }

    public class Mv
    {
        public long id { get; set; }
        public string vid { get; set; }
    }

    public class Pay
    {
        public long pay_down { get; set; }
        public long pay_month { get; set; }
        public long pay_play { get; set; }
        public long pay_status { get; set; }
        public long price_album { get; set; }
        public long price_track { get; set; }
        public long time_free { get; set; }
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
        public long chinesesinger { get; set; }
        public string desc { get; set; }
        public string desc_hilight { get; set; }
        public string docid { get; set; }
        public string es { get; set; }
        public File1 file { get; set; }
        public long fnote { get; set; }
        public long genre { get; set; }
        public long id { get; set; }
        public long index_album { get; set; }
        public long index_cd { get; set; }
        public long interval { get; set; }
        public long isonly { get; set; }
        public Ksong1 ksong { get; set; }
        public long language { get; set; }
        public string lyric { get; set; }
        public string lyric_hilight { get; set; }
        public string mid { get; set; }
        public Mv1 mv { get; set; }
        public string name { get; set; }
        public long newStatus { get; set; }
        public long nt { get; set; }
        public long ov { get; set; }
        public Pay1 pay { get; set; }
        public long pure { get; set; }
        public long sa { get; set; }
        public Singer[] singer { get; set; }
        public long status { get; set; }
        public string subtitle { get; set; }
        public long t { get; set; }
        public long tag { get; set; }
        public long tid { get; set; }
        public string time_public { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
        public long type { get; set; }
        public string url { get; set; }
        public long ver { get; set; }
        public Volume1 volume { get; set; }
    }

    public class Action1
    {
        public long alert { get; set; }
        public long icons { get; set; }
        public long msg { get; set; }
        public long _switch { get; set; }
    }

    public class Album1
    {
        public long id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string pmid { get; set; }
        public string subtitle { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
    }

    public class File1
    {
        public long b_30s { get; set; }
        public long e_30s { get; set; }
        public string media_mid { get; set; }
        public long size_128 { get; set; }
        public long size_128mp3 { get; set; }
        public long size_320 { get; set; }
        public long size_320mp3 { get; set; }
        public long size_aac { get; set; }
        public long size_ape { get; set; }
        public long size_dts { get; set; }
        public long size_flac { get; set; }
        public long size_ogg { get; set; }
        public long size_try { get; set; }
        public string strMediaMid { get; set; }
        public long try_begin { get; set; }
        public long try_end { get; set; }
    }

    public class Ksong1
    {
        public long id { get; set; }
        public string mid { get; set; }
    }

    public class Mv1
    {
        public long id { get; set; }
        public string vid { get; set; }
    }

    public class Pay1
    {
        public long pay_down { get; set; }
        public long pay_month { get; set; }
        public long pay_play { get; set; }
        public long pay_status { get; set; }
        public long price_album { get; set; }
        public long price_track { get; set; }
        public long time_free { get; set; }
    }

    public class Volume1
    {
        public float gain { get; set; }
        public float lra { get; set; }
        public float peak { get; set; }
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

    public class Singer1
    {
        public long id { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string title_hilight { get; set; }
        public long type { get; set; }
        public long uin { get; set; }
    }

    public class Zhida
    {
        public long type { get; set; }
        public Zhida_Singer zhida_singer { get; set; }
    }

    public class Zhida_Singer
    {
        public long albumNum { get; set; }
        public Hotalbum[] hotalbum { get; set; }
        public Hotsong[] hotsong { get; set; }
        public long mvNum { get; set; }
        public long singerID { get; set; }
        public string singerMID { get; set; }
        public string singerName { get; set; }
        public string singerPic { get; set; }
        public string singername_hilight { get; set; }
        public long songNum { get; set; }
    }

    public class Hotalbum
    {
        public long albumID { get; set; }
        public string albumMID { get; set; }
        public string albumName { get; set; }
        public string albumname_hilight { get; set; }
    }

    public class Hotsong
    {
        public string f { get; set; }
        public long songID { get; set; }
        public string songMID { get; set; }
        public string songName { get; set; }
        public string songname_hilight { get; set; }
    }
}