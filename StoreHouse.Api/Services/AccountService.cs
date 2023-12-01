using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using StoreHouse.Api.Model.DTO.AcсountDTO;
using StoreHouse.Api.Model.DTO.ManageDTO;
using StoreHouse.Api.Services.Interfaces;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;

namespace StoreHouse.Api.Services;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IOrganizationService _organizationService;
    private readonly IRoleService _roleService;

    public AccountService(IRoleService roleService, IOrganizationService organizationService, IUserService userService, IMapper mapper)
    {
        _roleService = roleService;
        _organizationService = organizationService;
        _mapper = mapper;
        _userService = userService;
    }
    public async Task<(bool IsSuccess, string ErrorMessage, ManageUserResponse User)> LoginUser(LoginDataRequest loginData)
    {
        try
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
        catch (Exception e)
        {
            return (false, e.Message, new ManageUserResponse());
        }
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage)> AddOrganizationAsync(OrganizationRequest organization)
    {
        try
        {
            var organizationMap = _mapper.Map<Organization>(organization);
            if (organizationMap == null)
                return (false, "Mapping failed, object is null");
            
            var addOrganization =
                await _organizationService.CreateOrganizationAsync(organizationMap);
            if (!addOrganization.IsSuccess)
                return (false, addOrganization.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage)> AddOwnerAsync(OwnerRequest user)
    {
        try
        {
            //Map User
            var userMap = _mapper.Map<User>(user);
            if (userMap == null)
                return (false, "Mapping failed, object was null");

            //Change required Data
            var roleId = await _roleService.GetRoleIdByName(user.RoleName);
            if (!roleId.IsSuccess)
                return (false, roleId.ErrorMessage);

            var organizationId = await _organizationService.GetOrganizationIdByNameAsync(user.OrganizationName);
            if (!organizationId.IsSuccess)
                return (false, roleId.ErrorMessage);
            
            userMap.RoleId = roleId.RoleId;
            userMap.OrganizationId = organizationId.OrganizationId;
            if (user.Password != null)
                userMap.HashedPassword = HashPassword(user.Password);

            //Call the DAL update service
            var addUser = await _userService.CreateUserAsync(userMap);
            if (!addUser.IsSuccess)
                return (false, addUser.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteOrganizationAsync(int organizationId)
    {
        try
        {
            var deletedOrganization = await _organizationService.DeleteOrganizationAsync(organizationId);
            if (!deletedOrganization.IsSuccess)
                return (false, deletedOrganization.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
    
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}