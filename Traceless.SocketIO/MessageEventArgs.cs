using Traceless.SocketIO.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.SocketIO
{
    public class MessageEventArgs : EventArgs
    {
        public IMessage Message { get; private set; }

        public MessageEventArgs(IMessage msg)
            : base()
        {
            this.Message = msg;
        }
    }
}