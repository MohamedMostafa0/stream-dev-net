using System;
using Socket_Lib.Helpers;

namespace Socket_Lib.Core.Events
{
    public abstract class SocketBaseArgs : EventArgs
    {
        public SocketEventType SocketEventType { get; private set; }
        public SocketBaseArgs(SocketEventType socketEventType)
        {
            SocketEventType = socketEventType;
        }
    }
}
