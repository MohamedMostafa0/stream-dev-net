using System.Net.Sockets;

namespace Socket_Lib.Core.Sockets
{
    public class NetSocket : BaseSocket
    {
        public NetSocket(Socket socket, short bufferLength) : base(socket, bufferLength)
        {
            Receive();
        }
    }
}
