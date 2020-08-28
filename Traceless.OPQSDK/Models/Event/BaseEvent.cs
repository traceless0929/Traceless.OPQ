using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    public class BaseEvent
    {
        public EventMsgBase EventMsg { get; set; }
        public EventType EventName { get; set; }
    }

    public class BaseEvent<T>
    {
        public T EventData { get; set; }
        public EventMsgBase EventMsg { get; set; }
        public EventType EventName { get; set; }
    }
}