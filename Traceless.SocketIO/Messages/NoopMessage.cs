using Traceless.SocketIO.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.SocketIO.Messages
{
    public class NoopMessage : Message
    {
        public NoopMessage()
        {
            this.MessageType = SocketIOMessageTypes.Noop;
        }

        public static NoopMessage Deserialize(string rawMessage)
        {
            return new NoopMessage();
        }
    }
}