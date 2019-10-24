using System;
using System.Threading;
using WebsocketAsyncClient_Library.Core;
using WebsocketAsyncClient_Library.Events;
using WebsocketAsyncClient_Library.Events.Args;
using WebsocketAsyncClient_Library.Helpers;

namespace TestWebsocket
{
    class Program
    {
        private static Thread _startThread;

        private static WebsocketClient _client;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnApplicationExit);

            Console.Write("Press 1 To start ... ");
            string input = Console.ReadLine();
            if (input == "1")
            {
                StartClient("ws://127.0.0.1:2628/Chat");
            }
            else if (input == "2")
            {
                StartClient("ws://127.0.0.1:2628/Project");
            }
            else if (input == "3")
            {
                StartClient("ws://127.0.0.1:2628/Project/2");
            }
        }

        private static void StartClient(string path)
        {
            _startThread = new Thread(new ParameterizedThreadStart(StartThread));
            _startThread.Start(path);
        }

        private static void StartThread(object path)
        {
            string p = (string)path;
            Console.WriteLine(p);
            _client = new WebsocketClient();
            WebsocketEvents.WebsocketEvent += OnWebsocketEvent;
            _client.StartAsync(p);

            if (_client.IsConnected)
            {
                Console.Write("Write");
            }

            Thread.Sleep(2000);

            while (_client.IsConnected)
            {
                //string message = Console.ReadLine();
                ClientSendMessage("hi");
                Thread.Sleep(1);
            }
        }

        private static void ClientSendMessage(string message)
        {
            _client.SendMessageToAllClientsInBehaviour(message);
        }

        private static void OnWebsocketEvent(object sender, BaseArgs e)
        {
            switch (e.WebsocketArgsType)
            {
                case WebsocketArgsType.Connection:
                    ConnectionEvents((ConnectionEventArgs)e);
                    break;
                case WebsocketArgsType.Message:
                    MessageEvents((MessageEventargs)e);
                    break;
                case WebsocketArgsType.Error:
                    ErrorEvents((ErrorEventargs)e);
                    break;
                default:
                    break;
            }
        }

        private static void ConnectionEvents(ConnectionEventArgs e)
        {
            switch (e.ConnectionEventType)
            {
                case ConnectionEventType.Connect:
                    Console.WriteLine($"Client Connected");
                    break;
                case ConnectionEventType.Connecting:
                    Console.WriteLine($"Client Connecting");
                    break;
                case ConnectionEventType.Disconnect:
                    Console.WriteLine($"Client DisConnected");
                    break;
                default:
                    break;
            }
        }
        private static void MessageEvents(MessageEventargs e)
        {
            switch (e.MessageEventType)
            {
                case MessageEventType.Sent:
                    // Console.WriteLine($"Message Sent : {e.Data}");
                    break;
                case MessageEventType.Receieved:
                    Console.WriteLine($"Message Recieved {e.Data}");
                    break;
                default:
                    break;
            }
        }
        private static void ErrorEvents(ErrorEventargs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.ProcessExit -= new EventHandler(OnApplicationExit);
        }
    }
}
