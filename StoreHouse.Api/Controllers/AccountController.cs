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
    private readonly ITokenLifetimeManager _tokenLifetimeManager;

    public AccountController(ITokenLifetimeManager tokenLifetimeManager, IAccountService accountService)
    {
        _accountService = accountService;
        _tokenLifetimeManager = tokenLifetimeManager;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDataRequest loginData)
    {
        var loginUser = await _accountService.LoginUser(loginData);
        if (!loginUser.IsSuccess)
            return BadRequest(loginUser.ErrorMessage);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginUser.User.Login),
            new Claim(ClaimTypes.Role, loginUser.User.RoleName)
        };
        var jwt = new JwtSecurityToken(
            issuer: "MyIssuer",
            audience: "MyClient",
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somesuperstrongkey")), SecurityAlgorithms.HmacSha256));

        string token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return Ok(new {loginUser.User, token});
    }
    
    [Authorize]
    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        string bearerToken  = HttpContext.Request.Headers["Authorization"];
        var token =
            bearerToken.Replace( "Bearer ", string.Empty, StringComparison.InvariantCultureIgnoreCase );

        var signOut = _tokenLifetimeManager.SignOut(new JwtSecurityToken(token));
        if (!signOut.IsSuccess)
            return BadRequest(signOut.ErrorManage);
        
        return Ok("Token revoked");
    }
}