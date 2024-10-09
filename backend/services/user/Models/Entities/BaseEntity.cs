using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Users.Models.Entities;
public class BaseEntity
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("createdBy")]
    public string CreatedBy { get; set; }

    [BsonElement("updatedBy")]
    public string UpdatedBy { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}


