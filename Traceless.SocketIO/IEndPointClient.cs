using Traceless.SocketIO.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.SocketIO
{
    public interface IEndPointClient
    {
        void On(string eventName, Action<IMessage> action);

        void Emit(string eventName, dynamic payload, Action<dynamic> callBack = null);

        void Send(IMessage msg);
    }
}