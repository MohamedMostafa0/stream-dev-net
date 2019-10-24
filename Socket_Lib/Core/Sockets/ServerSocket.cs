using System;
using System.Net.Sockets;
using Socket_Lib.Helpers;

namespace Socket_Lib.Core.Sockets
{
    public class ServerSocket : BaseSocket
    {
        public ServerSocket(short bufferLength, int localPort) : base(bufferLength)
        {
            Bind(localPort);
            Accept(localPort);
        }
        private void Accept(int localPort)
        {
            try
            {
                MainSocket.BeginAccept(OnAccept, localPort);
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Accept, ex.Message);
            }
        }
        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                int localPort = (int)ar.AsyncState;
                Socket acceptedSocket = MainSocket.EndAccept(ar);
                NetSocket netSocket = new NetSocket(acceptedSocket, BufferLength);
                FireAccept(netSocket);
                Accept(localPort);
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Accept, ex.Message);
            }
        }
    }
}
