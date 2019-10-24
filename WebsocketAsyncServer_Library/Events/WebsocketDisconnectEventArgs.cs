using WebsocketAsyncServer_Library.Helpers;

namespace WebsocketAsyncServer_Library.Events
{
    public class WebsocketDisconnectEventArgs : WebsocketBaseEventArgs
    {
        public string Reason { get; private set; }
        public bool WasClean { get; set; }
        public WebsocketDisconnectEventArgs(WebsocketEventType websocketEventType ,
            string reason ,
            bool wasClean) : base(websocketEventType)
        {
            Reason = reason;
            WasClean = wasClean;
        }
    }
}
