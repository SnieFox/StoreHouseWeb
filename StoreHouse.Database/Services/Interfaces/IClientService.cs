using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with Client table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IClientService
{
 //Client Category methods
 Task<(bool IsSuccess, string ErrorMessage, Client AddClient)> CreateClientAsync(Client client);
 Task<(bool IsSuccess, string ErrorMessage, Client UpdatedClient)> UpdateClientAsync(Client updatedClient);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteClientAsync(int clientId);
 Task<(bool IsSuccess, string ErrorMessage, List<Client> ClientList)> GetAllClientsAsync();
}