using System;
using WebsocketAsyncClient_Library.Helpers;

namespace WebsocketAsyncClient_Library.Events.Args
{
    public abstract class BaseArgs : EventArgs
    {
        public WebsocketArgsType WebsocketArgsType { get; set; }
    }
}
