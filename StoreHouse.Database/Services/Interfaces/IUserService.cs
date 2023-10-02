using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Represents a service for working with User table.
 * The methods of writing, reading and modifying table data are implemented.
 */
public interface IUserService
{
 //User methods
 Task<(bool IsSuccess, string ErrorMessage)> CreateUserAsync(User user);
 Task<(bool IsSuccess, string ErrorMessage)> UpdateUserAsync(User updatedUser);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteUserAsync(int userId);
 Task<(bool IsSuccess, string ErrorMessage, List<User> UserList)> GetAllUsersAsync();
}