using Users.Models.Enums;

namespace Users.Models.DTO.User;

public class UpdateUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfilePic { get; set; }
    public IEnumerable<Role>? Roles { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsLocked { get; set; }
    public string? Password { get; set; }
}
