using AutoMapper;
using Login.Models.DTO.User;
using Login.Models.Entities;
using Login.Models.Enums;
using Login.Services.UserService;
using Microsoft.AspNetCore.Mvc;
namespace Login.Controllers;

[Route("userapi/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet(Name = "getUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        if (users == null)
        {
            return NotFound();
        }
        var userList = _mapper.Map<IEnumerable<UserList>>(users);
        return Ok(userList);
    }

    [HttpGet("{id}", Name = "getUser")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var userDetails = _mapper.Map<UserDetails>(user);
        return Ok(userDetails);
    }

    [HttpGet("{id}", Name = "getUserDetails")]
    public async Task<IActionResult> getUserDetails(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return Ok(new { Message = "User not found.!" });
        }

        if (user.Roles.Contains(Role.Admin))
        {
            var users = await _userService.GetUsersAsync();
            var result = new
            {
                userDetails = _mapper.Map<UserDetails>(user),
                userList = _mapper.Map<IEnumerable<UserList>>(users)
            };
            return Ok(result);
        }
        else
        {
            var result = new
            {
                userDetails = _mapper.Map<UserDetails>(user),
            };
            return Ok(result);
        }
    }
    [HttpPost(Name = "loginUser")]
    public async Task<IActionResult> LoginUser([FromBody] LoginModel login)
    {
        var (user, message) = await _userService.GetAuthenticatedUserAsync(login);
        return Ok(new { User = user, Message = message });
    }

    [HttpPost(Name = "createUser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUser user)
    {
        if (user == null)
        {
            return BadRequest("Invalid user data");
        }
        var mapUser = _mapper.Map<Users>(user);
        await _userService.CreateUserAsync(mapUser);
        return RedirectToAction("getUserDetails", new { id = mapUser.Id });
    }
    [HttpGet(Name = "createBulkUser")]
    public async Task<IActionResult> CreateBulkUser()
    {
        await _userService.CreateBulkUserAsync(true, 10);
        return Ok(true);
    }

    [HttpPut("{id}", Name = "updateUser")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUser user)
    {
        if (user == null)
        {
            return BadRequest("Invalid user data");
        }

        var existingUser = await _userService.GetUserByIdAsync(id);

        if (existingUser == null)
        {
            return NotFound();
        }
        _mapper.Map(user, existingUser);
        await _userService.UpdateUserAsync(id, existingUser);
        return Ok(existingUser);
    }

    [HttpDelete("{id}", Name = "deleteUser")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var existingUser = await _userService.GetUserByIdAsync(id);

        if (existingUser == null)
        {
            return NotFound();
        }

        await _userService.DeleteUserAsync(id);
        return NoContent();
    }


    [HttpPut(Name = "updateUserPassword")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdatePassword userPassword)
    {
        if (userPassword == null)
        {
            return BadRequest("Invalid user data");
        }

        bool result = await _userService.UpdateUserPassword(userPassword);
        return Ok(result);
    }

    [HttpPut("{id}", Name = "updateUserPermission")]
    public async Task<IActionResult> UpdateUserPermission(string id, [FromBody] IEnumerable<Role> roles)
    {
        if (roles == null)
        {
            return BadRequest("Invalid user data");
        }

        var existingUser = await _userService.GetUserByIdAsync(id);

        if (existingUser == null)
        {
            return NotFound();
        }
        existingUser.Roles = roles;
        var mapUser = _mapper.Map<Users>(existingUser);
        await _userService.UpdateUserAsync(id, mapUser);
        return Ok(true);

    }

}
