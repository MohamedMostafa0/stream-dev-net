using WebsocketAsyncServer_Library.Helpers;

namespace WebsocketAsyncServer_Library.Events
{
    public class WebsocketOpenEventArgs : WebsocketBaseEventArgs
    {
        public WebsocketOpenEventArgs(WebsocketEventType websocketEventType) : base(websocketEventType)
        {
        }
    }
}
