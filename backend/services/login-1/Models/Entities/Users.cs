using login.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace login.Models.Entities
{
    public class Users : BaseEntity
    {
        [BsonElement("userID")]
        public string? UserID { get; set; }
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
        [BsonElement("company")]
        public string? Company { get; set; }
        [BsonElement("jobTitle")]
        public string? JobTitle { get; set; }
        [BsonElement("officeNumber")]
        public string? OfficeNumber { get; set; }
        [BsonElement("phoneNumber")]
        public string? PhoneNumber { get; set; }
        [BsonElement("alternateName")]
        public string? AlternateName { get; set; }
        [BsonElement("alternateNumber")]
        public string? AlternateNumber { get; set; }
        
        [BsonElement("isLocked")]
        public bool IsLocked { get; set; }
        [BsonElement("password")]
        public string? Password { get; set; }
        [BsonElement("isPasswordReset")]
        public bool IsPasswordReset { get; set; }
        [BsonElement("invalidPasswordAttempts")]
        public int InvalidPasswordAttempts { get; set; }
        [BsonElement("lastLoginDate")]
        public DateTime? LastLoginDate { get; set; }
        [BsonElement("passwordHistory")]
        public string[]? PasswordHistory { get; set; }

    }
}
