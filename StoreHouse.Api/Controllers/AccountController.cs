using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreHouse.Api.Model.DTO.AcoountDTO;
using StoreHouse.Api.Model.DTO.ManageDTO;
using StoreHouse.Api.Services.Interfaces;

namespace StoreHouse.Api.Controllers;

[ApiController]
[Route($"account")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService) => _accountService = accountService;
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDataRequest loginData)
    {
        var loginUser = await _accountService.LoginUser(loginData);
        if (!loginUser.IsSuccess)
            return BadRequest(loginUser.ErrorMessage);

        var token = GenerateJwtToken(loginUser.User, "http://localhost:5211", "http://localhost:5211");

        return Ok(new { Token = token, User = loginUser.User });
    }

    private string GenerateJwtToken(ManageUserResponse user, string validIssuer, string validAudience)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("your_secret_key");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.RoleName)
                // Добавьте другие нужные вам клеймы
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Время жизни токена
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = validIssuer, // Добавлено
            Audience = validAudience // Добавлено
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    [Authorize]
    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
}