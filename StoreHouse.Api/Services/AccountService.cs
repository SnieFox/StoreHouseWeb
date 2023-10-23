using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using StoreHouse.Api.Model.DTO.AcoountDTO;
using StoreHouse.Api.Model.DTO.ManageDTO;
using StoreHouse.Api.Services.Interfaces;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;

namespace StoreHouse.Api.Services;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public AccountService(IUserService userService, IMapper mapper)
    {
        _mapper = mapper;
        _userService = userService;
    }
    public async Task<(bool IsSuccess, string ErrorMessage, ManageUserResponse User)> LoginUser(LoginDataRequest loginData)
    {
        string hashedPassword = HashPassword(loginData.Password);
        
        var users = await _userService.GetAllUsersAsync();
        if (!users.IsSuccess)
            return (false, users.ErrorMessage, new ManageUserResponse());
        
        var checkedUser = users.UserList
            .FirstOrDefault(u => u.HashedPassword == hashedPassword && u.Login == loginData.Login);
        if (checkedUser == null)
            return (false, "Incorrect Login or Password", new ManageUserResponse());
        
        //Map Users
        var userMap = _mapper.Map<ManageUserResponse>(checkedUser);
        if (userMap == null)
            return (false, "Mapping failed, object was null", new ManageUserResponse());

        return (true, string.Empty, userMap);
    }
    
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}