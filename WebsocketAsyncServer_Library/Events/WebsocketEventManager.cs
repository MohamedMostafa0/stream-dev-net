using WebsocketAsyncServer_Library.Helpers;

namespace WebsocketAsyncServer_Library.Events
{
    public static class WebsocketEventManager
    {
        private static event WebsocketAction _websocketAcrionEventHandler;
        public static event WebsocketAction WebsocketAcrionEventHandler
        {
            add => _websocketAcrionEventHandler += value;
            remove => _websocketAcrionEventHandler -= value;
        }
        public static void FireWebsocketAction(WebsocketBaseEventArgs args)
        {
            _websocketAcrionEventHandler?.Invoke(args);
        }
    }
}
