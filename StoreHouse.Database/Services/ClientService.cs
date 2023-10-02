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
        await _context.Clients.AddAsync(client);
        
        var saved = await _context.SaveChangesAsync();
        return saved == 0 ? 
                        (false, "Something went wrong when adding to db", client) : 
                        (true, string.Empty, client);
    }

    //Update Client in Database
    public async Task<(bool IsSuccess, string ErrorMessage, Client UpdatedClient)> UpdateClientAsync(Client updatedClient)
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
        
        return saved == 0 ? 
                        (false, $"Something went wrong when updating Client {updatedClient.Id} to db", updatedClient) : 
                        (true, string.Empty, updatedClient);
    }

    //Delete Client from Database
    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteClientAsync(int clientId)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
        if (client == null) return (false, "Client does not exist");

        _context.Clients.Remove(client);
        var saved = await _context.SaveChangesAsync();
        
        return saved == 0 ? 
                        (false, "Something went wrong when deleting from db") : 
                        (true, string.Empty);
    }

    //Get all Clients from Database
    public async Task<(bool IsSuccess, string ErrorMessage, List<Client> ClientList)> GetAllClientsAsync()
    {
        var clients = await _context.Clients.ToListAsync();
        
        return (true, string.Empty, clients);
    }
}