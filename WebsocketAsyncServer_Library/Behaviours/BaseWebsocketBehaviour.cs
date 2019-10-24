using System;
using WebsocketAsyncServer_Library.Events;
using WebsocketAsyncServer_Library.Helpers;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebsocketAsyncServer_Library.Behaviours
{
    public class BaseWebsocketBehaviour : WebSocketBehavior
    {
        public virtual void BroadcastMessageAll(string message)
        {
            Sessions.Broadcast(message);
        }

        public virtual void BroadcastMessageAllAsync(string message)
        {
            Sessions.BroadcastAsync(message, OnBroadcastComplete);
        }

        private void OnBroadcastComplete()
        {
        }

        public virtual void SendMessageToClient(string message , string id)
        {
            foreach (var item in Sessions.IDs)
            {
                Console.WriteLine(item);
            }
            Sessions.SendTo(message , id);
        }

        public virtual void SendMessageToClientAsync(string message, string id)
        {
            foreach (var item in Sessions.IDs)
            {
                Console.WriteLine(item);
            }
            Sessions.SendToAsync(message, id , OnSendComplete);
        }

        private void OnSendComplete(bool sent)
        {
        }

        protected override void OnOpen()
        {
            WebsocketOpenEventArgs wo = new WebsocketOpenEventArgs(WebsocketEventType.Connect);
            WebsocketEventManager.FireWebsocketAction(wo);
            base.OnOpen();
        }
        protected override void OnClose(CloseEventArgs e)
        {
            WebsocketDisconnectEventArgs wd = new WebsocketDisconnectEventArgs(WebsocketEventType.Disconnect , e.Reason , e.WasClean);
            WebsocketEventManager.FireWebsocketAction(wd);
            base.OnClose(e);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            WebsocketErrorEventArgs we = new WebsocketErrorEventArgs(WebsocketEventType.Error, e.Message);
            WebsocketEventManager.FireWebsocketAction(we);
            base.OnError(e);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            WebsocketMessageEventArgs wm = new WebsocketMessageEventArgs(WebsocketEventType.Receive,
                e.Data,
                e.IsBinary,
                e.IsPing,
                e.IsText,
                e.RawData);
            WebsocketEventManager.FireWebsocketAction(wm);
            BroadcastMessageAllAsync(e.Data);
            base.OnMessage(e);
        }
    }
}
