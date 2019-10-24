using MouseDev_Server_Socket.Core.Helpers;
using Socket_Lib.Core.Buffering;
using Socket_Lib.Core.Events;
using Socket_Lib.Core.Sockets;
using Socket_Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MouseDev_Server_Socket.Core
{
    public class Room
    {
        private readonly Dictionary<string, NetSocket> netSockets;
        private readonly string roomId;
        public Room(string roomId)
        {
            this.roomId = roomId;
            netSockets = new Dictionary<string, NetSocket>();
        }

        public async Task<bool> Add(string id, NetSocket client)
        {
            NetSocket c = await FindSocket(id);
            // TODO throw exceptions
            if (c != null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                netSockets.Add(id, client);
            }
            client.SocketActionEventHandler += NetSocket_SocketActionEventHandler;
            return await Task.FromResult(true);
        }

        public async Task<bool> Remove(string id)
        {
            if (netSockets.ContainsKey(id))
            {
                netSockets.Remove(id);
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
        public async Task<NetSocket> FindSocket(string id)
        {
            if (netSockets.TryGetValue(id, out NetSocket client))
            {
                return await Task.FromResult(client);
            }
            return await Task.FromResult<NetSocket>(null);
        }
        public async Task<bool> Send(WritePacket wr)
        {
            if (netSockets.Count > 0)
            {
                foreach (var item in netSockets)
                {
                    item.Value.Send(wr);
                }
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> Send(string id, WritePacket wr)
        {
            NetSocket client = await FindSocket(id);
            if (client != null)
            {
                client.Send(wr);
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        #region NormalSocketEvent
        private async void NetSocket_SocketActionEventHandler(SocketBaseArgs args)
        {
            switch (args.SocketEventType)
            {
                case SocketEventType.Exception:
                    await HandleException();
                    break;
                case SocketEventType.Socket:
                    await HandleSocket((SocketBaseBehaviourArgs)args);
                    break;
                default:
                    break;
            }
        }
        private async Task HandleException()
        {
            await Task.FromResult(true);
        }
        private async Task HandleSocket(SocketBaseBehaviourArgs args)
        {
            switch (args.SocketBehaviourType)
            {
                case SocketBehaviourType.Connect:
                    await HandleConnect((SocketConnectEventArgs)args);
                    break;
                case SocketBehaviourType.Accept:
                    await HandleAccept((SocketAcceptEventArgs)args);
                    break;
                case SocketBehaviourType.Send:
                    await HandleSend((SocketSendEventArgs)args);
                    break;
                case SocketBehaviourType.Receive:
                    await HandleReceive((SocketReceiveEventArgs)args);
                    break;
                case SocketBehaviourType.Close:
                    await HandleClose((SocketCloseEventArgs)args);
                    break;
                default:
                    break;
            }
            await Task.FromResult(true);
        }

        private async Task HandleConnect(SocketConnectEventArgs args)
        {
            await Task.FromResult(true);
        }
        private async Task HandleAccept(SocketAcceptEventArgs args)
        {
            await Task.FromResult(true);
        }
        private async Task HandleSend(SocketSendEventArgs args)
        {
            await Task.FromResult(true);
        }
        private async Task HandleReceive(SocketReceiveEventArgs args)
        {
            await Task.FromResult(true);
        }
        private async Task HandleClose(SocketCloseEventArgs args)
        {
            await Task.FromResult(true);
        }
        #endregion
    }
}
