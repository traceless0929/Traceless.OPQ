using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    public class UserInfoResp
    {
        public int code { get; set; }
        public Data data { get; set; }
        public int @default { get; set; }
        public string message { get; set; }
        public int subcode { get; set; }
    }

    public class Data
    {
        public int astro { get; set; }
        public string avatarUrl { get; set; }
        public string bitmap { get; set; }
        public int commfrd { get; set; }
        public int friendship { get; set; }
        public string from { get; set; }
        public int gender { get; set; }
        public int greenvip { get; set; }
        public int intimacyScore { get; set; }
        public int isFriend { get; set; }
        public string logolabel { get; set; }
        public string nickname { get; set; }
        public int qqvip { get; set; }
        public int qzone { get; set; }
        public string realname { get; set; }
        public string smartname { get; set; }
        public long uin { get; set; }
    }
}