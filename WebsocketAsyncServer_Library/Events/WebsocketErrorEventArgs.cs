using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebsocketAsyncServer_Library.Helpers;

namespace WebsocketAsyncServer_Library.Events
{
    public class WebsocketErrorEventArgs : WebsocketBaseEventArgs
    {
        public string Message { get; set; }
        public WebsocketErrorEventArgs(WebsocketEventType websocketEventType , string message) : base(websocketEventType)
        {
            Message = message;
        }
    }
}
