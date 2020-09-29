using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    public class UserInfoResp
    {
        public long code { get; set; }
        public Data data { get; set; }
        public long @default { get; set; }
        public string message { get; set; }
        public long subcode { get; set; }
    }

    public class Data
    {
        public long astro { get; set; }
        public string avatarUrl { get; set; }
        public string bitmap { get; set; }
        public long commfrd { get; set; }
        public long friendship { get; set; }
        public string from { get; set; }
        public long gender { get; set; }
        public long greenvip { get; set; }
        public long intimacyScore { get; set; }
        public long isFriend { get; set; }
        public string logolabel { get; set; }
        public string nickname { get; set; }
        public long qqvip { get; set; }
        public long qzone { get; set; }
        public string realname { get; set; }
        public string smartname { get; set; }
        public long uin { get; set; }
    }
}