using WebsocketAsyncClient_Library.Events.Args;

namespace WebsocketAsyncClient_Library.Events
{
    public delegate void WebsocketEventHandler(object sender, BaseArgs e);

    public static class WebsocketEvents
    {
        private static event WebsocketEventHandler _websocketEvent;
        public static event WebsocketEventHandler WebsocketEvent
        {
            add
            {
                _websocketEvent += value;
            }
            remove
            {
                _websocketEvent -= value;
            }
        }
        public static void FireWebsocketEvent(object sender, BaseArgs e)
        {
            _websocketEvent?.Invoke(sender, e);
        }
    }
}
