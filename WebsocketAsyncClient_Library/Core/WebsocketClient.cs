using System;
using System.Threading;
using WebsocketAsyncClient_Library.Events;
using WebsocketAsyncClient_Library.Events.Args;
using WebsocketAsyncClient_Library.Helpers;
using WebSocketSharp;

namespace WebsocketAsyncClient_Library.Core
{
    public class WebsocketClient : IDisposable
    {
        protected WebSocket _webSocketClient;
        protected volatile bool _isRunning;

        public WebsocketClient()
        {
        }

        public virtual void Start(string url)
        {
            try
            {
                if (!_isRunning)
                {
                    _isRunning = true;
                    _webSocketClient = new WebSocket(url);

                    _webSocketClient.OnOpen += OnOpen;
                    _webSocketClient.OnClose += OnClose;
                    _webSocketClient.OnMessage += OnMessageEvent;
                    _webSocketClient.OnError += OnError;
                    WebsocketEvents.FireWebsocketEvent(_webSocketClient, new ConnectionEventArgs
                    {
                       ConnectionEventType = ConnectionEventType.Connecting,
                       WebsocketArgsType = WebsocketArgsType.Connection
                    });
                    _webSocketClient.Connect();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        public virtual void StartAsync(string url)
        {
            try
            {
                if (!_isRunning)
                {
                    _isRunning = true;
                    _webSocketClient = new WebSocket(url);

                    _webSocketClient.OnOpen += OnOpen;
                    _webSocketClient.OnClose += OnClose;
                    _webSocketClient.OnMessage += OnMessageEvent;
                    _webSocketClient.OnError += OnError;

                    WebsocketEvents.FireWebsocketEvent(_webSocketClient, new ConnectionEventArgs
                    {
                        ConnectionEventType = ConnectionEventType.Connecting,
                        WebsocketArgsType = WebsocketArgsType.Connection,
                    });

                    _webSocketClient.ConnectAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public virtual void Stop()
        {
            if (_webSocketClient != null)
            {
                _webSocketClient.OnOpen -= OnOpen;
                _webSocketClient.OnClose -= OnClose;
                _webSocketClient.OnMessage -= OnMessageEvent;
                _webSocketClient.OnError -= OnError;
                _webSocketClient.Close();

                WebsocketEvents.FireWebsocketEvent(_webSocketClient, new ConnectionEventArgs
                {
                    ConnectionEventType = ConnectionEventType.Disconnect,
                    WebsocketArgsType = WebsocketArgsType.Connection,
                });
            }
        }
        public virtual void StopAsync()
        {
            if (_webSocketClient != null)
            {
                _webSocketClient.OnOpen -= OnOpen;
                _webSocketClient.OnClose -= OnClose;
                _webSocketClient.OnMessage -= OnMessageEvent;
                _webSocketClient.OnError -= OnError;
                _webSocketClient.CloseAsync();

                WebsocketEvents.FireWebsocketEvent(_webSocketClient, new ConnectionEventArgs
                {
                    ConnectionEventType = ConnectionEventType.Disconnect,
                    WebsocketArgsType = WebsocketArgsType.Connection,
                });
            }
        }

        public virtual void SendMessageToAllClientsInBehaviourAsync(string message)
        {
            _webSocketClient.SendAsync(message, SendMessageToAllClientsInBehaviourAsyncCallback);
        }
        public virtual void SendMessageToAllClientsInBehaviour(string message)
        {
            _webSocketClient.Send(message);
        }
        protected virtual void SendMessageToAllClientsInBehaviourAsyncCallback(bool sucess)
        {

        }

        protected virtual void OnMessageEvent(object sender, MessageEventArgs e)
        {
            WebsocketEvents.FireWebsocketEvent(sender, new MessageEventargs()
            {
                Data = e.Data,
                DataRaw = e.RawData,
                MessageEventType = MessageEventType.Receieved,
                WebsocketArgsType = WebsocketArgsType.Message,
            });
        }
        protected virtual void OnOpen(object sender, EventArgs e)
        {
            WebsocketEvents.FireWebsocketEvent(sender, new ConnectionEventArgs
            {
                ConnectionEventType = ConnectionEventType.Connect,
            });
        }
        protected virtual void OnError(object sender, ErrorEventArgs e)
        {
            WebsocketEvents.FireWebsocketEvent(sender, new ErrorEventargs()
            {
                Exception = e.Exception,
                Message = e.Message,
            });
            Console.WriteLine(e.Message);
        }
        protected virtual void OnClose(object sender, CloseEventArgs e)
        {
            WebsocketEvents.FireWebsocketEvent(sender, new ConnectionEventArgs
            {
                ConnectionEventType = ConnectionEventType.Disconnect,
            });
            _isRunning = false;
        }

        public void Dispose()
        {
            StopAsync();
            while (!_isRunning)
            {
                Thread.Sleep(200);
            }
        }

        public bool IsConnected
        {
            get
            {
                return _webSocketClient.IsAlive;
            }
        }
    }
}
