using Microsoft.IdentityModel.Tokens;

namespace StoreHouse.Api.Services.Interfaces;

public interface ITokenLifetimeManager
{
    (bool IsSuccess, string ErrorManage) SignOut(SecurityToken securityToken);

    bool ValidateTokenLifetime(DateTime? notBefore,
        DateTime? expires,
        SecurityToken securityToken,
        TokenValidationParameters validationParameters);
}