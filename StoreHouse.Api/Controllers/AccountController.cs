using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreHouse.Api.Model.DTO.AcoountDTO;
using StoreHouse.Api.Services.Interfaces;

namespace StoreHouse.Api.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService) => _accountService = accountService;
    
    [HttpGet]
    public async Task<IActionResult> Login(LoginDataRequest loginData)
    {
        var loginUser = await _accountService.LoginUser(loginData);
        if (!loginUser.IsSuccess)
            return BadRequest(loginUser.ErrorMessage);
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, loginUser.User.Login),
                    new(ClaimTypes.Role, loginUser.User.RoleName)
                }, CookieAuthenticationDefaults.AuthenticationScheme)));
        
        return Ok(loginUser.User);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
}