namespace WebsocketAsyncClient_Library.Helpers
{
    public enum WebsocketArgsType : byte
    {
        Connection,
        Message,
        Error
    }
    public enum ConnectionEventType : byte
    {
        Connect,
        Connecting,
        Disconnect
    }
    public enum MessageEventType : byte
    {
        Sent,
        Receieved
    }
}
