using System;
using System.Collections.Generic;
using System.Text;
using Traceless.OPQSDK.Models.Msg;

namespace Traceless.OPQSDK.Models.Event
{
    public delegate int GroupMsgEvent(GroupMsg msg, long CurrentQQ);
}