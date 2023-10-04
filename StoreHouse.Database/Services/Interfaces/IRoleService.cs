using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with Role table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IRoleService
{
 //Role methods
 Task<(bool IsSuccess, string ErrorMessage, Role Role)> CreateRoleAsync(Role role);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteRoleAsync(int roleId);
 Task<(bool IsSuccess, string ErrorMessage, List<Role> RoleList)> GetAllRolesAsync();
}