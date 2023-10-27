using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

public interface IOrganizationService
{
    Task<(bool IsSuccess, string ErrorMessage, Organization organization)> CreateOrganizationAsync(Organization organization);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteOrganizationAsync(int organizationId);
    Task<(bool IsSuccess, string ErrorMessage, int OrganizationId)> GetOrganizationIdByUserLoginAsync(string userLogin);
    Task<(bool IsSuccess, string ErrorMessage, int OrganizationId)> GetOrganizationIdByNameAsync(string organizationName);
}