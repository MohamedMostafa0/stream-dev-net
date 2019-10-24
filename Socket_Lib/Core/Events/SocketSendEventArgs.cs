using Socket_Lib.Helpers;

namespace Socket_Lib.Core.Events
{
    public class SocketSendEventArgs : SocketBaseBehaviourArgs
    {
        public SocketSendEventArgs(SocketEventType socketEventType, 
            SocketBehaviourType socketBehaviourType) : base(socketEventType, socketBehaviourType)
        {
        }
    }
}
