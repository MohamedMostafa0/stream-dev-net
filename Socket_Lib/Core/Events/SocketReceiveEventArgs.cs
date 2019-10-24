using Socket_Lib.Helpers;
using Socket_Lib.Core.Buffering;

namespace Socket_Lib.Core.Events
{
    public class SocketReceiveEventArgs : SocketBaseBehaviourArgs
    {
        public ReadPacket ReadPacket { get; private set; }
        public SocketReceiveEventArgs(SocketEventType socketEventType, 
            SocketBehaviourType socketBehaviourType,
            ReadPacket readPacket) : base(socketEventType, socketBehaviourType)
        {
            ReadPacket = readPacket;
        }
    }
}
