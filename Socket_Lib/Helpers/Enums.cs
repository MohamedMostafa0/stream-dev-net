namespace Socket_Lib.Helpers
{
    public enum SocketEventType : byte
    {
        Exception, Socket
    }
    public enum SocketBehaviourType : byte
    {
        Connect, Accept, Send, Receive, Close
    }
    public enum ExceptionType : byte
    {
        Init , Bind , Connect , Accept , Receive , Send , Close
    }
}
