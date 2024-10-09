using Users.Models.DTO.User;
using Users.Models.Entities;
using Users.Models.Enums;

namespace Users.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task DeleteUserAsync(string id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(string id, User user);
        Task<(User user, string message)> GetAuthenticatedUserAsync(Login login);
        Task CreateBulkUserAsync(bool deletePreviousRecord, int totalRecord);
        Task<bool> UpdateUserPassword(UpdatePassword userPassword);
        Task<bool> UpdateUserPermission(string id, IEnumerable<Role> roles);
    }
}
