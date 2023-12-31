﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreHouse.Api.Model.DTO.AcсountDTO;
using StoreHouse.Api.Services.Interfaces;

namespace StoreHouse.Api.Controllers;

[ApiController]
[Route($"account")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ITokenLifetimeManager _tokenLifetimeManager;
    private readonly IConfiguration _configuration;

    public AccountController(ITokenLifetimeManager tokenLifetimeManager, IAccountService accountService, IConfiguration configuration)
    {
        _accountService = accountService;
        _tokenLifetimeManager = tokenLifetimeManager;
        _configuration = configuration;
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
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value)), SecurityAlgorithms.HmacSha256));

        string token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return Ok(new {loginUser.User, token});
    }
    
    [HttpPost]
    [Route("organization/create")]
    [Authorize(Roles = "System")]
    public async Task<IActionResult> AddOrganization(OrganizationRequest organization)
    {
        var addOrganization = await _accountService.AddOrganizationAsync(organization);
        if (!addOrganization.IsSuccess)
            return BadRequest(addOrganization.ErrorMessage);
        
        return Ok();
    }
    
    [HttpPost]
    [Route("owner/create")]
    [Authorize(Roles = "System")]
    public async Task<IActionResult> AddOwner(OwnerRequest owner)
    {
        var addOwner = await _accountService.AddOwnerAsync(owner);
        if (!addOwner.IsSuccess)
            return BadRequest(addOwner.ErrorMessage);
        
        return Ok();
    }
    
    [HttpDelete]
    [Route("organization/delete")]
    [Authorize(Roles = "System")]
    public async Task<IActionResult> DeleteOrganization(int organizationId)
    {
        var deleteOrganization = await _accountService.DeleteOrganizationAsync(organizationId);
        if (!deleteOrganization.IsSuccess)
            return BadRequest(deleteOrganization.ErrorMessage);
        
        return Ok();
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