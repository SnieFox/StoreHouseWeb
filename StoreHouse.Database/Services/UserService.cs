using Microsoft.EntityFrameworkCore;
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
 
    //Add User to Database
    public async Task<(bool IsSuccess, string ErrorMessage, User User)> CreateUserAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);

            var saved = await _context.SaveChangesAsync();
            return saved == 0 ? (false, "Something went wrong when adding to db", user) : (true, string.Empty, user);
        }
        catch (Exception e)
        {
            return (false, e.Message, user);
        }
    }

    //Update User in Database
    public async Task<(bool IsSuccess, string ErrorMessage, User User)> UpdateUserAsync(User updatedUser)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == updatedUser.Id);
            if (user == null) return (false, "Client does not exist", updatedUser);

            user.RoleId = updatedUser.RoleId;
            user.FullName = updatedUser.FullName;
            user.HashedLogin = updatedUser.HashedLogin;
            user.HashedPassword = updatedUser.HashedPassword;
            user.FullName = updatedUser.FullName;
            user.LastLoginDate = updatedUser.LastLoginDate;
            user.PinCode = updatedUser.PinCode;
            user.Email = updatedUser.Email;
            var saved = await _context.SaveChangesAsync();

            return saved == 0
                            ? (false, $"Something went wrong when updating Client {updatedUser.Id} to db", updatedUser)
                            : (true, string.Empty, updatedUser);
        }
        catch (Exception e)
        {
            return (false, e.Message, updatedUser);
        }
    }

    //Delete User from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteUserAsync(int userId)
    {
        try
        {
            var user = await _context.Users
                            .Include(c => c.Receipts)
                            .FirstOrDefaultAsync(c => c.Id == userId);
            if (user == null) return (false, "User does not exist");

            _context.Users.Remove(user);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
    
    //Get all Clients from Database
    public async Task<(bool IsSuccess, string ErrorMessage, List<User> UserList)> GetAllUsersAsync()
    {
        try
        {
            var users = await _context.Users.ToListAsync();

            return (true, string.Empty, users);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<User>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, User User)> GetUserByLogin(string login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.HashedLogin == login);
        if(user == null)
            return (false, "There in no user with this login", new User());

        return (true, string.Empty, user);
    }
}