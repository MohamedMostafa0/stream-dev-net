using System;
using WebsocketAsyncServer_Library.Helpers;

namespace WebsocketAsyncServer_Library.Events
{
    public abstract class WebsocketBaseEventArgs : EventArgs
    {
        public WebsocketEventType WebsocketEventType { get;private set; }
        public WebsocketBaseEventArgs(WebsocketEventType websocketEventType)
        {
            WebsocketEventType = websocketEventType;
        }
    }
}
