using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Role table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class RoleService : IRoleService
{
 private readonly StoreHouseContext _context;
 public RoleService(StoreHouseContext context) => _context = context;
 
 public Task<(bool IsSuccess, string ErrorMessage)> CreateRoleAsync(Role role)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> DeleteRoleAsync(int roleId)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage, List<Role> RoleList)> GetAllRolesAsync()
 {
  throw new NotImplementedException();
 }
}