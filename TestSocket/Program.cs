using Socket_Lib.Core.Buffering;
using Socket_Lib.Core.Events;
using Socket_Lib.Core.Sockets;
using Socket_Lib.Helpers;
using System;

namespace TestSocket
{
    class Program
    {
        private static BaseSocket tcpSocket;
        private static string Id;
        private static string matchId;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Id : ");
            Id = Console.ReadLine();
            Console.WriteLine("Enter Match Id : ");
            matchId = Console.ReadLine();
            tcpSocket = new ConnectSocket(512 , "127.0.0.1"  , 1618);
            tcpSocket.SocketActionEventHandler += TcpSocket_OnSocketAction;

            Console.ReadLine();
        }
        private static void TcpSocket_OnSocketAction(SocketBaseArgs args)
        {
            switch (args.SocketEventType)
            {
                case SocketEventType.Exception:
                     HandleException();
                    break;
                case SocketEventType.Socket:
                     HandleTCPSocket((SocketBaseBehaviourArgs)args);
                    break;
                default:
                    break;
            }
        }

        private static void HandleTCPSocket(SocketBaseBehaviourArgs args)
        {
            switch (args.SocketBehaviourType)
            {
                case SocketBehaviourType.Connect:
                    HandleTCPConnect((SocketConnectEventArgs)args);
                    break;
                case SocketBehaviourType.Accept:
                     HandleTCPAccept((SocketAcceptEventArgs)args);
                    break;
                case SocketBehaviourType.Send:
                     HandleTCPSend((SocketSendEventArgs)args);
                    break;
                case SocketBehaviourType.Receive:
                     HandleTCPReceive((SocketReceiveEventArgs)args);
                    break;
                case SocketBehaviourType.Close:
                     HandleTCPClose((SocketCloseEventArgs)args);
                    break;
                default:
                    break;
            }
        }

        private static void HandleTCPConnect(SocketConnectEventArgs args)
        {
            Console.WriteLine("Connected To Server");
        }
        private static void HandleTCPAccept(SocketAcceptEventArgs args)
        {
        }
        private static void HandleTCPSend(SocketSendEventArgs args)
        {
        }
        private static void HandleTCPReceive(SocketReceiveEventArgs args)
        {
            switch (args.ReadPacket.OpCode)
            {
                case 0:
                    string guid = args.ReadPacket.ReadString();
                    Console.WriteLine("Received : {0}", guid);
                    WritePacket wr = new WritePacket(0);
                    wr.AddString(guid);
                    wr.AddString(matchId);
                    wr.AddString(Id);
                    tcpSocket.Send(wr);
                    break;
                case 1:
                    Console.WriteLine("You Can Play Now");
                    break;
                default:
                    break;
            }
        }
        private static void HandleTCPClose(SocketCloseEventArgs args)
        {
        }

        private static void HandleException()
        {
            Console.WriteLine("Error");
        }
    }
}
