using Socket_Lib.Helpers;

namespace Socket_Lib.Core.Events
{
    public class SocketConnectEventArgs : SocketBaseBehaviourArgs
    {
        public SocketConnectEventArgs(SocketEventType socketEventType,
            SocketBehaviourType socketBehaviourType) : base(socketEventType, socketBehaviourType)
        {
        }
    }
}
