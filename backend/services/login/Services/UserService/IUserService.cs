using Login.Models.DTO.User;
using Login.Models.Entities;
using Login.Models.Enums;

namespace Login.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetUsersAsync();
        Task<Users> GetUserByIdAsync(string id);
        Task DeleteUserAsync(string id);
        Task CreateUserAsync(Users user);
        Task UpdateUserAsync(string id, Users user);
        Task<(Users user, string message)> GetAuthenticatedUserAsync(LoginModel login);
        Task CreateBulkUserAsync(bool deletePreviousRecord, int totalRecord);
        Task<bool> UpdateUserPassword(UpdatePassword userPassword);
        Task<bool> UpdateUserPermission(string id, IEnumerable<Role> roles);
    }
}
