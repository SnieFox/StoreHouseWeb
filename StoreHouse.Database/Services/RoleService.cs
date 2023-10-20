using Microsoft.EntityFrameworkCore;
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
 
    //Add Role to Database
    public async Task<(bool IsSuccess, string ErrorMessage, Role Role)> CreateRoleAsync(Role role)
    {
        try
        {
            await _context.Roles.AddAsync(role);

            var saved = await _context.SaveChangesAsync();
            return saved == 0 ? (false, "Something went wrong when adding to db", role) : (true, string.Empty, role);
        }
        catch (Exception e)
        {
            return (false, e.Message, role);
        }
    }

    //Delete Role from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteRoleAsync(int roleId)
    {
        try
        {
            var role = await _context.Roles
                            .Include(d => d.Users)
                            .FirstOrDefaultAsync(d => d.Id == roleId);
            if (role == null) return (false, "Role does not exist");

            _context.Roles.Remove(role);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    //Get all Roles from Database
    public async Task<(bool IsSuccess, string ErrorMessage, List<Role> RoleList)> GetAllRolesAsync()
    {
        try
        {
            var roles = await _context.Roles
                            .Include(c => c.Users)
                            .ToListAsync();

            return (true, string.Empty, roles);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<Role>());
        }
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage, int RoleId)> GetRoleIdByName(string name)
    {
        if (!await _context.Roles.AnyAsync(s => s.Name == name))
            return (false, "No role with this name", -1);
        
        var roleId = await _context.Roles
            .Where(c => c.Name == name)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        
        return (true, string.Empty, roleId);
    }
}