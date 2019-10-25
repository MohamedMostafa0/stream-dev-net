using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace MouseDev_Server_Api.Database.Models
{
    public class Client : BaseModel
    {
        [BsonElement("Username")]
        [BsonRepresentation(BsonType.String)]
        public string Username { get; set; }
        [BsonElement("Email")]
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }
        [BsonElement("IsVerified")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsVerified { get; set; }
        [BsonElement("HPS")]
        [BsonRepresentation(BsonType.String)]
        public string HPS { get; set; }
        [BsonElement("LastTimeOpened")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime LastTimeOpened { get; set; }
    }
}
