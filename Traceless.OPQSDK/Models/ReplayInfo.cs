using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models
{
    public class ReplayInfo
    {
        public int MsgSeq { get; set; }
        public int MsgTime { get; set; }
        public int UserID { get; set; }
        public string RawContent { get; set; }
    }
}