using System;

namespace WebsocketAsyncClient_Library.Events.Args
{
    public class ErrorEventargs : BaseArgs
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
