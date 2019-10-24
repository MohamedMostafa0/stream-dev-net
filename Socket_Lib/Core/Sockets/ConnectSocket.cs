using System;
using Socket_Lib.Core.Buffering;
using Socket_Lib.Helpers;

namespace Socket_Lib.Core.Sockets
{
    public class ConnectSocket : BaseSocket
    {
        public ConnectSocket(short bufferLength, string remoteAddress, int remotePort) : base(bufferLength)
        {
            Connect(remoteAddress, remotePort);
        }
        private void Connect(string remoteAddress, int remotePort)
        {
            try
            {
                MainSocket.BeginConnect(remoteAddress, remotePort, OnConnect, null);
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Connect, ex.Message);
            }
        }
        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                MainSocket.EndConnect(ar);
                FireConnect();
                Receive();
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Connect, ex.Message);
            }
        }
    }
}
