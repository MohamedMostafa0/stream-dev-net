using Socket_Lib.Core.Events;
using Socket_Lib.Helpers;
using System;
using System.Net;
using System.Net.Sockets;
using Socket_Lib.Core.Buffering;
using System.Threading;
using System.Diagnostics;

namespace Socket_Lib.Core.Sockets
{
    public abstract class BaseSocket : IDisposable
    {
        #region Properties
        public Socket MainSocket { get; private set; }
        public readonly byte[] Buffer;
        public short BufferLength { get; private set; }
        #endregion
        #region Events
        private event SocketAction _socketActionEventHandler;
        public event SocketAction SocketActionEventHandler
        {
            add => _socketActionEventHandler += value;
            remove => _socketActionEventHandler -= value;
        }
        private void FireSocketAction(SocketBaseArgs args)
        {
            _socketActionEventHandler?.Invoke(args);
        }
        protected void FireException(ExceptionType exceptionType, string exceptionMessage)
        {
            SocketExceptionEventArgs se = new SocketExceptionEventArgs(SocketEventType.Exception, exceptionType, exceptionMessage);
            FireSocketAction(se);
        }
        protected void FireAccept(NetSocket netSocket)
        {
            SocketAcceptEventArgs sa = new SocketAcceptEventArgs(SocketEventType.Socket, SocketBehaviourType.Accept, netSocket);
            FireSocketAction(sa);
        }
        protected void FireConnect()
        {
            SocketConnectEventArgs sc = new SocketConnectEventArgs(SocketEventType.Socket, SocketBehaviourType.Connect);
            FireSocketAction(sc);
        }
        private void FireSend()
        {
            SocketSendEventArgs ss = new SocketSendEventArgs(SocketEventType.Socket, SocketBehaviourType.Send);
            FireSocketAction(ss);
        }
        private void FireReceive(ReadPacket readPacket)
        {
            SocketReceiveEventArgs sr = new SocketReceiveEventArgs(SocketEventType.Socket, SocketBehaviourType.Receive, readPacket);
            FireSocketAction(sr);
        }
        private void FireClose()
        {
            SocketCloseEventArgs scl = new SocketCloseEventArgs(SocketEventType.Socket, SocketBehaviourType.Close);
            FireSocketAction(scl);
        }
        #endregion
        #region Ctor
        public BaseSocket(short bufferLength)
        {
            try
            {
                BufferLength = bufferLength;
                Buffer = new byte[bufferLength];
                MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Init, ex.Message);
            }
        }
        public BaseSocket(Socket socket, short bufferLength)
        {
            try
            {
                BufferLength = bufferLength;
                Buffer = new byte[bufferLength];
                MainSocket = socket;
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Init, ex.Message);
            }
        }

        ~BaseSocket()
        {
            if (_socketActionEventHandler != null)
            {
                foreach (SocketAction item in _socketActionEventHandler.GetInvocationList())
                {
                    _socketActionEventHandler -= item;
                }
            }
            Close();
        }
        #endregion
        #region Bind
        protected void Bind(int localPort)
        {
            try
            {
                MainSocket.Bind(new IPEndPoint(IPAddress.Any, localPort));
                MainSocket.Listen(100);
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Bind, ex.Message);
            }
        }
        #endregion
        #region Send
        public void Send(WritePacket wr)
        {
            try
            {
                MainSocket.BeginSend(wr.Buffer, 0, wr.Buffer.Length, SocketFlags.None, OnSend, null);
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Send, ex.Message);
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                MainSocket.EndSend(ar);
                FireSend();
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Send, ex.Message);
            }
        }
        #endregion
        #region Receive
        protected void Receive()
        {
            try
            {
                MainSocket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, OnReceive, null);
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Receive, ex.Message);
            }
        }
        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                int len = MainSocket.EndReceive(ar);
                if (len > 0)
                {
                    ReadPacket readPacket = new ReadPacket(Buffer);
                    FireReceive(readPacket);
                    Receive();
                }
                else
                {
                    throw new Exception("Buffer length is zero");
                }
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Receive, ex.Message);
            }
        }
        #endregion
        #region Close
        public void Dispose()
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Close, ex.Message);
            }
        }
        private void Close()
        {
            try
            {
                if (MainSocket != null)
                {
                    if (MainSocket.Connected)
                    {
                        MainSocket.Disconnect(true);
                    }
                    else
                    {
                        MainSocket = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
                FireClose();
            }
            catch (Exception ex)
            {
                FireException(ExceptionType.Close, ex.Message);
            }
        }
        #endregion
    }
}
