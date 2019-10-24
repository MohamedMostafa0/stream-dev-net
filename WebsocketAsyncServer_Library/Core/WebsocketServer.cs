using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using WebsocketAsyncServer_Library.Behaviours;
using WebsocketAsyncServer_Library.Models;
using WebSocketSharp.Server;

namespace WebsocketAsyncServer_Library.Core
{
    public class WebsocketServer : IWebsocketServer
    {
        private HttpServer _websocketServer;
        private IDictionary<string, WebsocketServiceModel> _services;

        public bool Start(ICollection<WebsocketServiceModel> services, int port)
        {
            try
            {
                _services = new Dictionary<string, WebsocketServiceModel>();
                Stop();
                _websocketServer = new HttpServer(IPAddress.Loopback, port);
                foreach (var item in services)
                {
                    if (!string.IsNullOrEmpty(item.Path))
                    {
                        _services.Add(item.Name, item);
                        AddService(item.Behaviour, item.Path);
                    }
                }
                _websocketServer.Start();
                return true;
            }
            catch { }
            return false;
        }

        public bool Stop()
        {
            try
            {
                if (_websocketServer != null)
                {
                    _websocketServer.Stop();
                    _websocketServer = null;
                    return true;
                }
            }
            catch
            {

            }

            return false;
        }

        public void AddService<T>(T behaviour, string path) where T : BaseWebsocketBehaviour, new()
        {
            if (behaviour != null)
            {
                _websocketServer.AddWebSocketService<T>("/" + path , ()=>
                {
                    return new T();
                });
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
