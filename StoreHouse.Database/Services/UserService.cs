using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Represents a service for working with User table.
 * The methods of writing, reading and modifying table data are implemented.
 */
public class UserService : IUserService
{
 private readonly StoreHouseContext _context;
 public UserService(StoreHouseContext context) => _context = context;
 
 public Task<(bool IsSuccess, string ErrorMessage)> CreateUserAsync(User user)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> UpdateUserAsync(User updatedUser)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage)> DeleteUserAsync(int userId)
 {
  throw new NotImplementedException();
 }

 public Task<(bool IsSuccess, string ErrorMessage, List<User> UserList)> GetAllUsersAsync()
 {
  throw new NotImplementedException();
 }
}