using Traceless.SocketIO.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.SocketIO.Messages
{
    public class DisconnectMessage : Message
    {
        public override string Event
        {
            get { return "disconnect"; }
        }

        public DisconnectMessage() : base()
        {
            this.MessageType = SocketIOMessageTypes.Disconnect;
        }

        public DisconnectMessage(string endPoint)
            : this()
        {
            this.Endpoint = endPoint;
        }

        public static DisconnectMessage Deserialize(string rawMessage)
        {
            DisconnectMessage msg = new DisconnectMessage();
            // 0:: 0::/test
            msg.RawMessage = rawMessage;

            string[] args = rawMessage.Split(SPLITCHARS, 3);
            if (args.Length == 3)
            {
                if (!string.IsNullOrWhiteSpace(args[2]))
                    msg.Endpoint = args[2];
            }
            return msg;
        }

        public override string Encoded
        {
            get
            {
                return string.Format("0::{0}", this.Endpoint);
            }
        }
    }
}