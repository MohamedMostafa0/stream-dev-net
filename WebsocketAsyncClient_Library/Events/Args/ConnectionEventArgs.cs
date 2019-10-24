using WebsocketAsyncClient_Library.Helpers;

namespace WebsocketAsyncClient_Library.Events.Args
{
    public class ConnectionEventArgs : BaseArgs
    {
        public ConnectionEventType ConnectionEventType { get; set; }
    }
}
