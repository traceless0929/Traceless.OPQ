﻿using System.Collections.Generic;
using Traceless.OPQSDK.Models.Msg;

namespace Traceless.OPQSDK.Models.Content
{
    public class BaseContent
    {
        public string Tips { get; set; }

        public string Content { get; set; } = "";

        /// <summary>
        /// AT列表
        /// </summary>
        public List<long> UserID { get; set; } = new List<long>();

        public List<OPQCode> Codes => Content.ParseOPQCode();
    }
}