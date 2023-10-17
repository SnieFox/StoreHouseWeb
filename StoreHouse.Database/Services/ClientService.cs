using Microsoft.EntityFrameworkCore;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Services;

/*
 * Presents a service for working with Client table.
 * The methods of writing, reading and changing table data are implemented.
 */
public class ClientService : IClientService
{
    private readonly StoreHouseContext _context;
    public ClientService(StoreHouseContext context) => _context = context;
 
    //Add Client to Database
    public async Task<(bool IsSuccess, string ErrorMessage, Client AddClient)> CreateClientAsync(Client client)
    {
        try
        {
            await _context.Clients.AddAsync(client);

            var saved = await _context.SaveChangesAsync();
            return saved == 0
                ? (false, "Something went wrong when adding to db", client)
                : (true, string.Empty, client);
        }
        catch (Exception e)
        {
            return (false, e.Message, client);
        }
    }

    //Update Client in Database
    public async Task<(bool IsSuccess, string ErrorMessage, Client UpdatedClient)> UpdateClientAsync(Client updatedClient)
    {
        try
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == updatedClient.Id);
            if (client == null) return (false, "Client does not exist", updatedClient);

            client.Email = updatedClient.Email;
            client.BirthDate = updatedClient.BirthDate;
            client.Sex = updatedClient.Sex;
            client.BankCard = updatedClient.BankCard;
            client.FullName = updatedClient.FullName;
            client.MobilePhone = updatedClient.MobilePhone;
            var saved = await _context.SaveChangesAsync();

            return saved == 0
                            ? (false, $"Something went wrong when updating Client {updatedClient.Id} to db",
                                            updatedClient)
                            : (true, string.Empty, updatedClient);
        }
        catch (Exception e)
        {
            return (false, e.Message, updatedClient);
        }
    }

    //Delete Client from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteClientAsync(int clientId)
    {
        try
        {
            var client = await _context.Clients
                            .Include(c => c.Receipts)
                            .FirstOrDefaultAsync(c => c.Id == clientId);
            if (client == null) return (false, "Client does not exist");

            _context.Clients.Remove(client);
            var saved = await _context.SaveChangesAsync();

            return saved == 0 ? (false, "Something went wrong when deleting from db") : (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    //Get all Clients from Database
    public async Task<(bool IsSuccess, string ErrorMessage, List<Client> ClientList)> GetAllClientsAsync()
    {
        try
        {
            var clients = await _context.Clients
                .Include(c => c.Receipts)
                .ThenInclude(p => p.ProductLists)
                .ToListAsync();

            return (true, string.Empty, clients);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<Client>());
        }
    }
}