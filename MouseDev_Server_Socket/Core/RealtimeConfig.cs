using Socket_Lib.Core.Buffering;
using Socket_Lib.Core.Events;
using Socket_Lib.Core.Sockets;
using Socket_Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseDev_Server_Socket.Core
{
    internal class RealtimeConfig
    {
        private readonly BaseSocket tcpSocket;
        private readonly Dictionary<string, Room> RoomsDic;
        private readonly Dictionary<string, NetSocket> tempUsersDick;
        #region Ctor
        internal RealtimeConfig()
        {
            RoomsDic = new Dictionary<string, Room>();
            tempUsersDick = new Dictionary<string, NetSocket>();
            tcpSocket = new ServerSocket(NetManager.BUFFER_LENGTH, NetManager.PORT);
            tcpSocket.SocketActionEventHandler += TcpSocket_OnSocketAction;
        }
        #endregion

        #region Rooms
        private async Task<bool> AddRoom(string id, Room room)
        {
            if (RoomsDic.ContainsKey(id))
            {
                return await Task.FromResult(false);
            }
            else
            {
                RoomsDic.Add(id, room);
                return await Task.FromResult(true);
            }
        }
        private async Task<bool> AddClient(string roomId, string clientId, NetSocket netSocket)
        {
            if (RoomsDic.TryGetValue(roomId, out Room room))
            {
                return await room.Add(clientId, netSocket);
            }
            else
            {
                room = new Room(roomId);
                await room.Add(clientId, netSocket);
                return await AddRoom(roomId, room);
            }
        }
        #endregion

        #region Sockets
        #region TCP
        private async void TcpSocket_OnSocketAction(SocketBaseArgs args)
        {
            switch (args.SocketEventType)
            {
                case SocketEventType.Exception:
                    await HandleException();
                    break;
                case SocketEventType.Socket:
                    await HandleTCPSocket((SocketBaseBehaviourArgs)args);
                    break;
                default:
                    break;
            }
        }

        private async Task HandleTCPSocket(SocketBaseBehaviourArgs args)
        {
            switch (args.SocketBehaviourType)
            {
                case SocketBehaviourType.Accept:
                    await HandleTCPAccept((SocketAcceptEventArgs)args);
                    break;
                case SocketBehaviourType.Close:
                    await HandleTCPClose((SocketCloseEventArgs)args);
                    break;
                default:
                    break;
            }
            await Task.FromResult(true);
        }
        private async Task HandleTCPAccept(SocketAcceptEventArgs args)
        {
            // first Accept
            args.NetSocket.SocketActionEventHandler += NetSocket_RT_SocketActionEventHandler;
            await HandleTCPRT(args.NetSocket);
        }
        private async Task HandleTCPClose(SocketCloseEventArgs args)
        {
            await Task.FromResult(true);
        }
        #endregion

        private async Task HandleException()
        {
            await Task.FromResult(true);
        }
        private async Task HandleTCPRT(NetSocket netSocket)
        {
            Guid guid = Guid.NewGuid();
            tempUsersDick.Add(guid.ToString(), netSocket);
            // TODO Complete TCP Connection
            WritePacket wr = new WritePacket(NetManager.RT_PACKET_OPCODE0);
            wr.AddString(guid.ToString());
            netSocket.Send(wr);
            await Task.FromResult(true);
        }
        #endregion
        #region RT
        private async void NetSocket_RT_SocketActionEventHandler(SocketBaseArgs args)
        {
            switch (args.SocketEventType)
            {
                case SocketEventType.Exception:
                    await HandleException_RT();
                    break;
                case SocketEventType.Socket:
                    await HandleTCPSocket_RT((SocketBaseBehaviourArgs)args);
                    break;
                default:
                    break;
            }
        }

        private async Task HandleTCPSocket_RT(SocketBaseBehaviourArgs args)
        {
            switch (args.SocketBehaviourType)
            {
                case SocketBehaviourType.Send:
                    await HandleTCPSend_RT((SocketSendEventArgs)args);
                    break;
                case SocketBehaviourType.Receive:
                    await HandleTCPReceive_RT((SocketReceiveEventArgs)args);
                    break;
                case SocketBehaviourType.Close:
                    await HandleTCPClose_RT((SocketCloseEventArgs)args);
                    break;
                default:
                    break;
            }
            await Task.FromResult(true);
        }

        private async Task HandleTCPSend_RT(SocketSendEventArgs args)
        {
            await Task.FromResult(true);
        }
        private async Task HandleTCPReceive_RT(SocketReceiveEventArgs args)
        {
            switch (args.ReadPacket.OpCode)
            {
                case 0:
                    string guid = args.ReadPacket.ReadString();
                    string roomId = args.ReadPacket.ReadString();
                    string clientId = args.ReadPacket.ReadString();
                    if (tempUsersDick.TryGetValue(guid , out NetSocket netSocket))
                    {
                        tempUsersDick.Remove(guid);
                        await AddClient(roomId, clientId, new NetSocket(netSocket.MainSocket, NetManager.BUFFER_LENGTH));
                        WritePacket wr = new WritePacket(NetManager.RT_PACKET_OPCODE1);
                        netSocket.Send(wr);
                    }
                    else
                    {

                    }
                    break;
                default:
                    break;
            }
            await Task.FromResult(true);
        }
        private async Task HandleTCPClose_RT(SocketCloseEventArgs args)
        {
            await Task.FromResult(true);
        }
        #endregion
        private async Task HandleException_RT()
        {
            await Task.FromResult(true);
        }
    }
}
