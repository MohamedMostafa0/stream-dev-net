using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebsocketAsyncServer_Library.Helpers;

namespace WebsocketAsyncServer_Library.Events
{
    public class WebsocketMessageEventArgs : WebsocketBaseEventArgs
    {
        public string Data { get; private set; }
        public bool IsBinary { get; private set; }
        public bool IsPing { get; private set; }
        public bool IsText { get; private set; }
        public byte[] RawData { get; private set; }
        public WebsocketMessageEventArgs(WebsocketEventType websocketEventType,
            string data,
            bool isBinary ,
            bool isPing,
            bool isText,
            byte[] rawData) : base(websocketEventType)
        {
            Data = data;
            IsBinary = isBinary;
            IsPing = isPing;
            IsText = isText;
            RawData = rawData;
        }
    }
}
