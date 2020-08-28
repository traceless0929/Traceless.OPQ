using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.SocketIO
{
    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public ErrorEventArgs(string message) : base()
        {
            this.Message = message;
        }

        public ErrorEventArgs(string message, Exception exception) : base()
        {
            this.Message = message;
            this.Exception = exception;
        }
    }
}