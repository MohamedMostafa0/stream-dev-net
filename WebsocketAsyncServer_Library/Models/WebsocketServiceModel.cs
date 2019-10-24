using WebsocketAsyncServer_Library.Behaviours;

namespace WebsocketAsyncServer_Library.Models
{
    public class WebsocketServiceModel
    {
        public BaseWebsocketBehaviour Behaviour { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public WebsocketServiceModel(BaseWebsocketBehaviour behaviour ,
            string path , 
            string name)
        {
            Behaviour = behaviour;
            Path = path;
            Name = name;
        }
    }
}
