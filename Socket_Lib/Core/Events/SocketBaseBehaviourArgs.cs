using Socket_Lib.Helpers;

namespace Socket_Lib.Core.Events
{
    public abstract class SocketBaseBehaviourArgs : SocketBaseArgs
    {
        public SocketBehaviourType SocketBehaviourType { get; private set; }
        public SocketBaseBehaviourArgs(SocketEventType socketEventType , SocketBehaviourType socketBehaviourType) : base(socketEventType)
        {
            SocketBehaviourType = socketBehaviourType;
        }
    }
}
