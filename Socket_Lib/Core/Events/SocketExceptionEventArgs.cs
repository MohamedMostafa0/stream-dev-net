using Socket_Lib.Helpers;

namespace Socket_Lib.Core.Events
{
    public class SocketExceptionEventArgs : SocketBaseArgs
    {
        public ExceptionType ExceptionType { get; private set; }
        public string ExceptionMessage { get; private set; }
        public SocketExceptionEventArgs(SocketEventType socketEventType , 
            ExceptionType exceptionType ,
            string exceptionMessage) : base(socketEventType)
        {
            ExceptionType = exceptionType;
            ExceptionMessage = exceptionMessage;
        }
    }
}
