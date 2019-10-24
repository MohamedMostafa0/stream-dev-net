using Socket_Lib.Helpers;
using Socket_Lib.Core.Sockets;

namespace Socket_Lib.Core.Events
{
    public class SocketAcceptEventArgs : SocketBaseBehaviourArgs
    {
        public NetSocket NetSocket { get; private set; }
        public SocketAcceptEventArgs(SocketEventType socketEventType, 
            SocketBehaviourType socketBehaviourType,
            NetSocket netSocket) : base(socketEventType, socketBehaviourType)
        {
            NetSocket = netSocket;
        }
    }
}
