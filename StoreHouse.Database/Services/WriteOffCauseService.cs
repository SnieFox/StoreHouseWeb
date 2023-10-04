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
        await _context.WriteOffCauses.AddAsync(writeOffCause);
        
        var saved = await _context.SaveChangesAsync();
        return saved == 0 ? 
                        (false, "Something went wrong when adding to db", writeOffCause) : 
                        (true, string.Empty, writeOffCause);
    }

    //Delete WriteOffCause from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffCauseAsync(int writeOffCauseId)
    {
        var writeOffCause = await _context.WriteOffCauses
                        .Include(c => c.WriteOffs)
                        .FirstOrDefaultAsync(c => c.Id == writeOffCauseId);
        if (writeOffCause == null) return (false, "Client does not exist");

        _context.WriteOffCauses.Remove(writeOffCause);
        var saved = await _context.SaveChangesAsync();
        
        return saved == 0 ? 
                        (false, "Something went wrong when deleting from db") : 
                        (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<WriteOffCause> WriteOffCauseList)> GetAllWriteOffCausesAsync()
    {
        var writeOffCauses = await _context.WriteOffCauses.ToListAsync();
        
        return (true, string.Empty, writeOffCauses);
    }
}