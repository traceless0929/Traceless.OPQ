using Traceless.SocketIO.Helpers;
using Traceless.SocketIO.Messages;
using System;
using WebSocket4Net;

namespace Traceless.SocketIO
{
    /// <summary>
    /// C# Socket.IO client interface
    /// </summary>
	internal interface IClient
    {
        event EventHandler Opened;

        event EventHandler<MessageEventArgs> Message;

        event EventHandler SocketConnectionClosed;

        event EventHandler<ErrorEventArgs> Error;

        SocketIOHandshake HandShake { get; }
        bool IsConnected { get; }
        WebSocketState ReadyState { get; }

        void Connect();

        IEndPointClient Connect(string endPoint);

        void Close();

        void Dispose();

        void On(string eventName, Action<IMessage> action);

        void On(string eventName, string endPoint, Action<IMessage> action);

        void Emit(string eventName, dynamic payload);

        void Emit(string eventName, dynamic payload, string endPoint = "", Action<dynamic> callBack = null);

        void Send(IMessage msg);

        //void Send(string rawEncodedMessageText);
    }
}