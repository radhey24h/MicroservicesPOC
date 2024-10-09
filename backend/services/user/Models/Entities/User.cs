using Users.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Users.Models.Entities;

public class User : BaseEntity
{
    [BsonElement("firstName")]
    public string? FirstName { get; set; }
    [BsonElement("lastName")]
    public string? LastName { get; set; }
    [BsonElement("email")]
    public string? Email { get; set; }
    [BsonElement("profilePic")]
    public string? ProfilePic { get; set; }
    [BsonElement("role")]
    [BsonRepresentation(BsonType.String)]
    public IEnumerable<Role>? Roles { get; set; }
    [BsonElement("phoneNumber")]
    public string? PhoneNumber { get; set; }
    [BsonElement("isLocked")]
    public bool IsLocked { get; set; }
    [BsonElement("password")]
    public string? Password { get; set; }
}
