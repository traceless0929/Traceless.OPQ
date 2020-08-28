using Newtonsoft.Json;
using Traceless.SocketIO.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.SocketIO.Messages
{
    public class ConnectMessage : Message
    {
        public object ConnectMsg { get; private set; }

        public override string Event
        {
            get { return "connect"; }
        }

        public ConnectMessage() : base()
        {
            this.MessageType = SocketIOMessageTypes.Connect;
        }

        public ConnectMessage(string endPoint) : this()
        {
            this.Endpoint = endPoint;
        }

        public static ConnectMessage Deserialize(string rawMessage)
        {
            ConnectMessage msg = new ConnectMessage();
            msg.RawMessage = rawMessage;
            msg.ConnectMsg = JsonConvert.DeserializeObject<object>(rawMessage);
            return msg;
        }

        public override string Encoded
        {
            get
            {
                return string.Format("1::{0}{1}", this.Endpoint, string.Empty, string.Empty);
            }
        }
    }
}