using System;
using System.Collections.Generic;
using WebsocketAsyncServer_Library.Behaviours;
using WebsocketAsyncServer_Library.Models;

namespace WebsocketAsyncServer_Library.Core
{
    public interface IWebsocketServer : IDisposable
    {
        bool Start(ICollection<WebsocketServiceModel> services, int port);
        bool Stop();
        void AddService<T>(T behaviour, string path) where T : BaseWebsocketBehaviour, new();
    }
}