using WebsocketAsyncClient_Library.Helpers;

namespace WebsocketAsyncClient_Library.Events.Args
{
    public class MessageEventargs : BaseArgs
    {
        public MessageEventType MessageEventType { get; set; }
        public string Data { get; set; }
        public byte[] DataRaw { get; set; }
        public bool IsBinary { get; set; }
        public bool IsText { get; set; }
        public bool IsPing { get; set; }
    }
}
