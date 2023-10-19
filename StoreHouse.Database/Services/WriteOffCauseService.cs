using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Represents a service for working with WriteOffCause table.
 * The methods of writing, reading and modifying table data are implemented.
 */
public class WriteOffCauseService : IWriteOffCauseService
{
    private readonly StoreHouseContext _context;
    public WriteOffCauseService(StoreHouseContext context) => _context = context;
 
    //Add WriteOffCause to Database
    public async Task<(bool IsSuccess, string ErrorMessage, WriteOffCause WriteOffCause)> CreateWriteOffCauseAsync(WriteOffCause writeOffCause)
    {
        try
        {
            await _context.WriteOffCauses.AddAsync(writeOffCause);

            var saved = await _context.SaveChangesAsync();
            return saved == 0
                            ? (false, "Something went wrong when adding to db", writeOffCause)
                            : (true, string.Empty, writeOffCause);
        }
        catch (Exception e)
        {
            return (false, e.Message, writeOffCause);
        }
    }

    //Delete WriteOffCause from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffCauseAsync(int writeOffCauseId)
    {
        try
        {
            var writeOffCause = await _context.WriteOffCauses
                            .Include(c => c.WriteOffs)
                            .FirstOrDefaultAsync(c => c.Id == writeOffCauseId);
            if (writeOffCause == null) return (false, "Client does not exist");

            _context.WriteOffCauses.Remove(writeOffCause);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<WriteOffCause> WriteOffCauseList)> GetAllWriteOffCausesAsync()
    {
        try
        {
            var writeOffCauses = await _context.WriteOffCauses.ToListAsync();

            return (true, string.Empty, writeOffCauses);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<WriteOffCause>());
        }
    }
    
    //Get WriteOffCategoryId by Name
    public async Task<(bool IsSuccess, string ErrorMessage, int Id)> GetIdByName(string name)
    {
        if (!await _context.WriteOffCauses.AnyAsync(s => s.Name == name))
            return (false, "No write-off category with this name", -1);

        int id = await _context.WriteOffCauses
            .Where(s => s.Name == name)
            .Select(s => s.Id)
            .FirstOrDefaultAsync();

        return (true, string.Empty, id);
    }
}