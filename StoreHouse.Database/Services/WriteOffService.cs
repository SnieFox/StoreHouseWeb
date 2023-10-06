using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Extensions;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with WriteOff table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class WriteOffService : IWriteOffService
{
    private readonly StoreHouseContext _context;
    public WriteOffService(StoreHouseContext context) => _context = context;
 
    //Add WriteOff to Database
    public async Task<(bool IsSuccess, string ErrorMessage, WriteOff WriteOff)> CreateWriteOffAsync(WriteOff writeOff)
    {
        try
        {
            await _context.WriteOffs.AddAsync(writeOff);
            var saved = await _context.SaveChangesAsync();

            var updateResult =
                            await SupportingMethodExtension.UpdateRemainsAsync(_context, writeOff.ProductLists, false);
            if (!updateResult.IsSuccess)
                return (false, $"Update of Remains Failed. {updateResult.ErrorMessage}", writeOff);

            return saved == 0
                            ? (false, "Something went wrong when adding to db", writeOff)
                            : (true, string.Empty, writeOff);
        }
        catch (Exception e)
        {
            return (false, e.Message, writeOff);
        }
    }

    //Update WriteOff in Database
    public async Task<(bool IsSuccess, string ErrorMessage, WriteOff WriteOff)> UpdateWriteOffAsync(WriteOff updatedWriteOff)
    {
        try
        {
            var writeOff = await _context.WriteOffs.FirstOrDefaultAsync(c => c.Id == updatedWriteOff.Id);
            if (writeOff == null) return (false, "WriteOff does not exist", updatedWriteOff);

            writeOff.CauseId = updatedWriteOff.CauseId;
            writeOff.UserId = updatedWriteOff.UserId;
            writeOff.UserName = updatedWriteOff.UserName;
            writeOff.Date = updatedWriteOff.Date;
            writeOff.Comment = updatedWriteOff.Comment;
            var saved = await _context.SaveChangesAsync();

            return saved == 0
                            ? (false, $"Something went wrong when updating WriteOff {updatedWriteOff.Id} to db",
                                            updatedWriteOff)
                            : (true, string.Empty, updatedWriteOff);
        }
        catch (Exception e)
        {
            return (false, e.Message, updatedWriteOff);
        }
    }

    //Delete WriteOff from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteWriteOffAsync(int writeOffId)
    {
        try
        {
            var writeOff = await _context.WriteOffs
                            .Include(c => c.ProductLists)
                            .FirstOrDefaultAsync(c => c.Id == writeOffId);
            if (writeOff == null) return (false, "WriteOff does not exist");

            var updateResult =
                            await SupportingMethodExtension.UpdateRemainsAsync(_context, writeOff.ProductLists, true);
            if (!updateResult.IsSuccess) return (false, $"Update of Remains Failed. {updateResult.ErrorMessage}");

            _context.WriteOffs.Remove(writeOff);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    //Get all Clients from Database
    public async Task<(bool IsSuccess, string ErrorMessage, List<WriteOff> WriteOffList)> GetAllWriteOffsAsync()
    {
        try
        {
            var writeOffs = await _context.WriteOffs.ToListAsync();

            return (true, string.Empty, writeOffs);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<WriteOff>());
        }
    }
}