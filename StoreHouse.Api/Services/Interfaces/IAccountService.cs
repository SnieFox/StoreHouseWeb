using StoreHouse.Api.Model.DTO.AcсountDTO;
using StoreHouse.Api.Model.DTO.ManageDTO;
using StoreHouse.Database.Entities;

namespace StoreHouse.Api.Services.Interfaces;

public interface IAccountService
{
    Task<(bool IsSuccess, string ErrorMessage, ManageUserResponse User)> LoginUser(LoginDataRequest loginData);
    Task<(bool IsSuccess, string ErrorMessage)> AddOrganizationAsync(OrganizationRequest organization);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteOrganizationAsync(int organizationId);
    Task<(bool IsSuccess, string ErrorMessage)> AddOwnerAsync(OwnerRequest user);
}