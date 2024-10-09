using Users.Models.Enums;

namespace Users.Models.DTO.User;

public class CreateUser
{
    public string? UserID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? ProfilePic { get; set; }
    public IEnumerable<Role> Roles { get; set; }
    public string? Company { get; set; }
    public string? JobTitle { get; set; }
    public string? OfficeNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Pager { get; set; }
    public string? Dialer { get; set; }
    public string? AlternateName { get; set; }
    public string? AlternateNumber { get; set; }
    public bool IsLocked { get; set; }
}
