using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace MouseDev_Server_Api.Database.Models
{
    public class Project : BaseModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("ProjectName")]
        [BsonRepresentation(BsonType.String)]
        public string ProjectName { get; set; }
        [BsonElement("ProjectKey")]
        [BsonRepresentation(BsonType.String)]
        public string ProjectKey { get; set; }
        [BsonElement("ProjectSecret")]
        [BsonRepresentation(BsonType.String)]
        public string ProjectSecret { get; set; }
        [BsonElement("CreatedDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [BsonElement("LastTimeOpened")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime LastTimeOpened { get; set; }
        [BsonElement("Clients")]
        public ICollection<ClientRole> Clients { get; set; }
    }
}
