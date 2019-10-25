using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MouseDev_Server_Api.Database.Models
{
    public class ClientRole
    {
        [BsonRepresentation(BsonType.Document)]
        [BsonElement("Client")]
        public Client Client { get; set; }
        [BsonElement("RoleBehaviour")]
        public RoleBehaviour RoleBehaviour { get; set; }
    }
}
