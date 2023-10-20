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
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public ManageService(IClientService clientService, IUserService userService, IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _userService = userService;
        _clientService = clientService;
        _mapper = mapper;
    }
    public async Task<(bool IsSuccess, string ErrorMessage, List<ManageClientResponse> AllClients)> GetAllClientsAsync()
    {
        //Get all Clients
        var clients = await _clientService.GetAllClientsAsync();
        if (!clients.IsSuccess)
            return (false, clients.ErrorMessage, new List<ManageClientResponse>());
        
        //Map Clients
        var clientsMap = _mapper.Map<List<ManageClientResponse>>(clients);
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

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateClientAsync(ManageClientRequest updatedClient)
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

    public async Task<(bool IsSuccess, string ErrorMessage)> AddClientAsync(ManageClientRequest client)
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

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteClientAsync(int clientId)
    {
        var deletedClient = await _clientService.DeleteClientAsync(clientId);
        if (!deletedClient.IsSuccess)
            return (false, deletedClient.ErrorMessage);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<ManageUserResponse> AllUsers)> GetAllUsersAsync()
    {
        //Get all Users
        var users = await _userService.GetAllUsersAsync();
        if (!users.IsSuccess)
            return (false, users.ErrorMessage, new List<ManageUserResponse>());
        
        //Map Users
        var usersMap = _mapper.Map<List<ManageUserResponse>>(users);
        if (usersMap == null)
            return (false, "Mapping failed, object was null", new List<ManageUserResponse>());

        return (true, string.Empty, usersMap);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateUserAsync(ManageUserRequest updatedUser)
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

    public async Task<(bool IsSuccess, string ErrorMessage)> AddUserAsync(ManageUserRequest user)
    {
        //Map User
        var userMap = _mapper.Map<User>(user);
        if (userMap == null)
            return (false, "Mapping failed, object was null");
        
        //Change required Data
        var roleId = await _roleService.GetRoleIdByName(user.RoleName);
        if (!roleId.IsSuccess)
            return (false, roleId.ErrorMessage);
        
        
        userMap.RoleId = roleId.RoleId;
        if(user.Password != null)
            userMap.HashedPassword = HashPassword(user.Password);
        
        //Call the DAL update service
        var updateUser = await _userService.UpdateUserAsync(userMap);
        if (!updateUser.IsSuccess)
            return (false, updateUser.ErrorMessage);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteUserAsync(int userId)
    {
        var deletedUser = await _userService.DeleteUserAsync(userId);
        if (!deletedUser.IsSuccess)
            return (false, deletedUser.ErrorMessage);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<ManageRoleResponse> AllRoles)> GetAllRolesAsync()
    {
        //Get all Roles
        var roles = await _roleService.GetAllRolesAsync();
        if (!roles.IsSuccess)
            return (false, roles.ErrorMessage, new List<ManageRoleResponse>());
        
        //Map Roles
        var rolesMap = _mapper.Map<List<ManageRoleResponse>>(roles);
        if (rolesMap == null)
            return (false, "Mapping failed, object was null", new List<ManageRoleResponse>());

        return (true, string.Empty, rolesMap);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddRoleAsync(ManageRoleRequest role)
    {
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

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteRoleAsync(int roleId)
    {
        var deletedRole = await _roleService.DeleteRoleAsync(roleId);
        if (!deletedRole.IsSuccess)
            return (false, deletedRole.ErrorMessage);

        return (true, string.Empty);
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