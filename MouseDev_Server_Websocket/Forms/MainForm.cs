using System.Collections.Generic;
using System.Windows.Forms;
using WebsocketAsyncServer_Library.Behaviours;
using WebsocketAsyncServer_Library.Core;
using WebsocketAsyncServer_Library.Events;
using WebsocketAsyncServer_Library.Helpers;
using WebsocketAsyncServer_Library.Models;

namespace MouseDev_Server_Websocket.Forms
{
    public partial class MainForm : Form
    {
        private IWebsocketServer websocketServer;
        public MainForm()
        {
            InitializeComponent();
            WebsocketEventManager.WebsocketAcrionEventHandler += WebsocketEventManager_WebsocketAcrionEventHandler;
            websocketServer = new WebsocketServer();
            WebsocketServiceModel chatModel = new WebsocketServiceModel(new BehaviourChat(),
                "Chat",
                "Chat");
            WebsocketServiceModel projectModel = new WebsocketServiceModel(new ProjectBehaviour(),
              "Project",
              "Project");
            List<WebsocketServiceModel> behaviours = new List<WebsocketServiceModel>
            {
                chatModel,
                projectModel
            };
            websocketServer.Start(behaviours, 2628);
            WebsocketServiceModel projectModel2 = new WebsocketServiceModel(new ProjectBehaviour(),
             "Project/2",
             "Project");
            websocketServer.AddService(projectModel2.Behaviour , projectModel2.Path);
            MessageBox.Show("Server Started");
        }

        private void WebsocketEventManager_WebsocketAcrionEventHandler(WebsocketBaseEventArgs args)
        {
            switch (args.WebsocketEventType)
            {
                case WebsocketEventType.Connect:
                    HandleConnect((WebsocketOpenEventArgs)args);
                    break;
                case WebsocketEventType.Disconnect:
                    HandleDisconnect((WebsocketDisconnectEventArgs)args);
                    break;
                case WebsocketEventType.Receive:
                    HandleReceive((WebsocketMessageEventArgs)args);
                    break;
                case WebsocketEventType.Send:
                    break;
                case WebsocketEventType.Error:
                    HandleError((WebsocketErrorEventArgs)args);
                    break;
                default:
                    break;
            }
        }
        private void HandleConnect(WebsocketOpenEventArgs args)
        {
            MessageBox.Show("Connected");
        }
        private void HandleDisconnect(WebsocketDisconnectEventArgs args)
        {
            MessageBox.Show(string.Format("DisConnected Was Clean ? {0} Reason : {1}", args.WasClean, args.Reason));
        }

        private void HandleReceive(WebsocketMessageEventArgs args)
        {
            MessageBox.Show("Receive");
        }

        private void HandleError(WebsocketErrorEventArgs args)
        {
            MessageBox.Show(string.Format("Error : {0}", args.Message));
        }
    }
}
