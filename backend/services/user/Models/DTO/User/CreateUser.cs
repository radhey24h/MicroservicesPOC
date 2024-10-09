using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Users.Models.Enums;

namespace Users.Models.DTO.User;

public class CreateUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? ProfilePic { get; set; }
    public IEnumerable<Role>? Roles { get; set; } = new List<Role> { Role.Reader };
    public string? PhoneNumber { get; set; }
    public bool IsLocked { get; set; }
    public string? Password { get; set; }
}
