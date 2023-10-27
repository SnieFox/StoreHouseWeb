using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using StoreHouse.Api.Model.DTO.ManageDTO;
using StoreHouse.Api.Services.Interfaces;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;

namespace StoreHouse.Api.Services;

public class ManageService : IManageService
{
    private readonly IClientService _clientService;
    private readonly IOrganizationService _organizationService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public ManageService(IOrganizationService organizationService, IClientService clientService, IUserService userService, IRoleService roleService, IMapper mapper)
    {
        _organizationService = organizationService;
        _roleService = roleService;
        _userService = userService;
        _clientService = clientService;
        _mapper = mapper;
    }
    public async Task<(bool IsSuccess, string ErrorMessage, List<ManageClientResponse> AllClients)> GetAllClientsAsync()
    {
        try
        {
            //Get all Clients
            var clients = await _clientService.GetAllClientsAsync();
            if (!clients.IsSuccess)
                return (false, clients.ErrorMessage, new List<ManageClientResponse>());

            //Map Clients
            var clientsMap = _mapper.Map<List<ManageClientResponse>>(clients.ClientList);
            if (clientsMap == null)
                return (false, "Mapping failed, object was null", new List<ManageClientResponse>());

            //Change required Data
            foreach (var clientResponse in clientsMap)
            {
                foreach (var client in clients.ClientList.Where(wr => wr.Id == clientResponse.Id))
                {
                    decimal receiptsSum = 0;
                    foreach (var receipt in client.Receipts)
                    {
                        var sum = GetSum(receipt.ProductLists);
                        if (!sum.IsSuccess)
                            return (false, sum.ErrorMessage, new List<ManageClientResponse>());

                        receiptsSum += sum.Sum;
                    }

                    clientResponse.ReceiptSum = receiptsSum;
                }
            }

            return (true, string.Empty, clientsMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<ManageClientResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateClientAsync(ManageClientRequest updatedClient)
    {
        try
        {
            //Map Clients
            var clientMap = _mapper.Map<Client>(updatedClient);
            if (clientMap == null)
                return (false, "Mapping failed, object was null", -1);

            //Call the DAL update service
            var updateClient = await _clientService.UpdateClientAsync(clientMap);
            if (!updateClient.IsSuccess)
                return (false, updateClient.ErrorMessage, -1);

            return (true, string.Empty, updatedClient.Id);
        }
        catch (Exception e)
        {
            return (false, e.Message, -1);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddClientAsync(ManageClientRequest client)
    {
        try
        {
            //Map Clients
            var clientMap = _mapper.Map<Client>(client);
            if (clientMap == null)
                return (false, "Mapping failed, object was null");

            //Call the DAL update service
            var addClient = await _clientService.CreateClientAsync(clientMap);
            if (!addClient.IsSuccess)
                return (false, addClient.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteClientAsync(int clientId)
    {
        try
        {
            var deletedClient = await _clientService.DeleteClientAsync(clientId);
            if (!deletedClient.IsSuccess)
                return (false, deletedClient.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<ManageUserResponse> AllUsers)> GetAllUsersAsync(string userLogin)
    {
        try
        {
            //Get all Users
            var users = await _userService.GetAllUsersAsync();
            if (!users.IsSuccess)
                return (false, users.ErrorMessage, new List<ManageUserResponse>());
            
            var organizationId = await _organizationService.GetOrganizationIdByUserLoginAsync(userLogin);
            if (!organizationId.IsSuccess)
                return (false, users.ErrorMessage, new List<ManageUserResponse>());
            
            var systemUsers = users.UserList.Where(user => user.Role.Name == "System" || user.OrganizationId != organizationId.OrganizationId).ToList();
            foreach (var user in systemUsers)
            {
                users.UserList.Remove(user);
            }

            //Map Users
            var usersMap = _mapper.Map<List<ManageUserResponse>>(users.UserList);
            if (usersMap == null)
                return (false, "Mapping failed, object was null", new List<ManageUserResponse>());

            return (true, string.Empty, usersMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<ManageUserResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateUserAsync(ManageUserRequest updatedUser)
    {
        try
        {
            //Map User
            var userMap = _mapper.Map<User>(updatedUser);
            if (userMap == null)
                return (false, "Mapping failed, object was null", -1);

            //Change required Data
            var roleId = await _roleService.GetRoleIdByName(updatedUser.RoleName);
            if (!roleId.IsSuccess)
                return (false, roleId.ErrorMessage, -1);

            userMap.RoleId = roleId.RoleId;
            //Call the DAL update service
            var updateUser = await _userService.UpdateUserAsync(userMap);
            if (!updateUser.IsSuccess)
                return (false, updateUser.ErrorMessage, -1);

            return (true, string.Empty, updatedUser.Id);
        }
        catch (Exception e)
        {
            return (false, e.Message, -1);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddUserAsync(ManageUserRequest user, string userLogin)
    {
        try
        {
            if (user.RoleName == "Admin" && (user.Password == "" || user.Login == ""))
                return (false, "Admin must have Login and Password");
            //Map User
            var userMap = _mapper.Map<User>(user);
            if (userMap == null)
                return (false, "Mapping failed, object was null");

            //Change required Data
            var roleId = await _roleService.GetRoleIdByName(user.RoleName);
            if (!roleId.IsSuccess)
                return (false, roleId.ErrorMessage);

            var organizationId = await _organizationService.GetOrganizationIdByUserLoginAsync(userLogin);
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

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteUserAsync(int userId)
    {
        try
        {
            var deletedUser = await _userService.DeleteUserAsync(userId);
            if (!deletedUser.IsSuccess)
                return (false, deletedUser.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<ManageRoleResponse> AllRoles)> GetAllRolesAsync()
    {
        try
        {
            //Get all Roles
            var roles = await _roleService.GetAllRolesAsync();
            if (!roles.IsSuccess)
                return (false, roles.ErrorMessage, new List<ManageRoleResponse>());
            var rolesToDelete = roles.RoleList.Where(role => role.Name is "System" or "Owner").ToList();
            foreach (var role in rolesToDelete)
            {
                roles.RoleList.Remove(role);
            }

            //Map Roles
            var rolesMap = _mapper.Map<List<ManageRoleResponse>>(roles.RoleList);
            if (rolesMap == null)
                return (false, "Mapping failed, object was null", new List<ManageRoleResponse>());

            return (true, string.Empty, rolesMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<ManageRoleResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddRoleAsync(ManageRoleRequest role)
    {
        try
        {
            if (role.Name == "System")
                return (false, "System roles cannot be added");
            //Map Role
            var roleMap = _mapper.Map<Role>(role);
            if (roleMap == null)
                return (false, "Mapping failed, object was null");

            //Call the DAL update service
            var addRole = await _roleService.CreateRoleAsync(roleMap);
            if (!addRole.IsSuccess)
                return (false, addRole.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteRoleAsync(int roleId)
    {
        try
        {
            var deletedRole = await _roleService.DeleteRoleAsync(roleId);
            if (!deletedRole.IsSuccess)
                return (false, deletedRole.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
    
    private static (bool IsSuccess, string ErrorMessage, decimal Sum) GetSum(List<ProductList> productLists)
    {
        try
        {
            decimal sum = 0;

            foreach (var product in productLists)
            {
                sum += product.Price * (decimal)product.Count;
            }

            return (true, string.Empty, sum);
        }
        catch (Exception e)
        {
            return (false, e.Message, 0);
        }
    }
    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}