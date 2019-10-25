using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MouseDev_Server_Api.Database.Models
{
    public class RoleBehaviour
    {
        [BsonElement("DeleteProject")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool DeleteProject { get; set; }
        [BsonElement("CreateDatabase")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool CreateDatabase { get; set; }
        [BsonElement("CreateEvent")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool CreateEvent { get; set; }
        [BsonElement("CreateLeaderboard")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool CreateLeaderboard { get; set; }
        [BsonElement("CreateMultiplayerMatch")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool CreateMultiplayerMatch { get; set; }
        [BsonElement("UseOnlineTest")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool UseOnlineTest { get; set; }
        [BsonElement("UseCloudCode")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool UseCloudCode { get; set; }
    }
}
