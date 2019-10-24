using Socket_Lib.Helpers;

namespace Socket_Lib.Core.Events
{
    public class SocketCloseEventArgs : SocketBaseBehaviourArgs
    {
        public SocketCloseEventArgs(SocketEventType socketEventType,
            SocketBehaviourType socketBehaviourType) : base(socketEventType, socketBehaviourType)
        {
        }
    }
}
