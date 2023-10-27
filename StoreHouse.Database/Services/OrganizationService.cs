using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

public class OrganizationService : IOrganizationService
{
    private readonly StoreHouseContext _context;
    public OrganizationService(StoreHouseContext context) => _context = context;
    
    public async Task<(bool IsSuccess, string ErrorMessage, Organization organization)> CreateOrganizationAsync(Organization organization)
    {
        try
        {
            await _context.Organizations.AddAsync(organization);

            var saved = await _context.SaveChangesAsync();
            return saved == 0
                ? (false, "Something went wrong when adding to db", organization)
                : (true, string.Empty, organization);
        }
        catch (Exception e)
        {
            return (false, e.Message, organization);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteOrganizationAsync(int organizationId)
    {
        try
        {
            var organization = await _context.Organizations
                .Include(c => c.Users)
                .FirstOrDefaultAsync(d => d.Id == organizationId);
            if (organization == null) return (false, "Organization does not exist");

            _context.Organizations.Remove(organization);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage, int OrganizationId)> GetOrganizationIdByUserLoginAsync(string userLogin)
    {
        try
        {
            var organizationId = await _context.Users
                .Where(d => d.Login == userLogin)
                .Select(d => d.OrganizationId)
                .FirstOrDefaultAsync();
            if (organizationId == 0) return (false, "", -1);

            return (true, string.Empty, organizationId);
        }
        catch (Exception e)
        {
            return (false, e.Message, -1);
        }
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage, int OrganizationId)> GetOrganizationIdByNameAsync(string organizationName)
    {
        try
        {
            var organizationId = await _context.Organizations
                .Where(d => d.Name == organizationName)
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            if (organizationId == 0) return (false, "", -1);

            return (true, string.Empty, organizationId);
        }
        catch (Exception e)
        {
            return (false, e.Message, -1);
        }
    }
}