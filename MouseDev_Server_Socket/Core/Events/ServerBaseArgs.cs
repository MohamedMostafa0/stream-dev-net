using System;
using MouseDev_Server_Socket.Core.Helpers;

namespace MouseDev_Server_Socket.Core.Events
{
    public abstract class ServerBaseArgs : EventArgs
    {
        public ServerEventType ServerEventType { get; private set; }
        public ServerBaseArgs(ServerEventType serverEventType)
        {
            ServerEventType = serverEventType;
        }
    }
}
