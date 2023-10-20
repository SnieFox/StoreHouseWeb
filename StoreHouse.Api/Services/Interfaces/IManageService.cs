using StoreHouse.Api.Model.DTO.ManageDTO;

namespace StoreHouse.Api.Services.Interfaces;

public interface IManageService
{
    //CRUD for Clients
    Task<(bool IsSuccess, string ErrorMessage, List<ManageClientResponse> AllClients)> GetAllClientsAsync();
    Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateClientAsync(ManageClientRequest updatedClient);
    Task<(bool IsSuccess, string ErrorMessage)> AddClientAsync(ManageClientRequest client);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteClientAsync(int clientId);
    
    //CRUD for Users
    Task<(bool IsSuccess, string ErrorMessage, List<ManageUserResponse> AllUsers)> GetAllUsersAsync();
    Task<(bool IsSuccess, string ErrorMessage, int UpdatedId)> UpdateUserAsync(ManageUserRequest updatedUser);
    Task<(bool IsSuccess, string ErrorMessage)> AddUserAsync(ManageUserRequest user);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteUserAsync(int userId);
    
    //CRUD for Roles
    Task<(bool IsSuccess, string ErrorMessage, List<ManageRoleResponse> AllRoles)> GetAllRolesAsync();
    Task<(bool IsSuccess, string ErrorMessage)> AddRoleAsync(ManageRoleRequest role);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteRoleAsync(int roleId);
}