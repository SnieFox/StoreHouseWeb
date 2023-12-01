using Microsoft.AspNetCore.Mvc;
using StoreHouse.Api.Model.DTO.ManageDTO;
using StoreHouse.Api.Services.Interfaces;

namespace StoreHouse.Api.Controllers;

[ApiController]
[Route($"manage")]
//[Authorize]
public class ManageController : Controller
{
    private readonly IManageService _manageService;
    public ManageController(IManageService manageService) => _manageService = manageService;
    
    [HttpGet]
    [Route($"clients")]
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _manageService.GetAllClientsAsync();
        if (!clients.IsSuccess)
            return BadRequest(clients.ErrorMessage);
        
        return Ok(clients.AllClients);
    }
    
    [HttpGet]
    [Route($"users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var userLogin = HttpContext.User.Identity.Name;
        var users = await _manageService.GetAllUsersAsync(userLogin);
        if (!users.IsSuccess)
            return BadRequest(users.ErrorMessage);
        
        return Ok(users.AllUsers);
    }
    
    [HttpGet]
    [Route($"roles")]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _manageService.GetAllRolesAsync();
        if (!roles.IsSuccess)
            return BadRequest(roles.ErrorMessage);
        
        return Ok(roles.AllRoles);
    }
    
    [HttpPut]
    [Route("client/update")]
    public async Task<IActionResult> UpdateClient(ManageClientRequest updatedClient)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var updatedClientResult = await _manageService.UpdateClientAsync(updatedClient);
        if (!updatedClientResult.IsSuccess)
            return BadRequest(updatedClientResult.ErrorMessage);

        return Ok(updatedClientResult.UpdatedId);
    }
    
    [HttpPut]
    [Route("user/update")]
    public async Task<IActionResult> UpdateUser(ManageUserRequest updatedUser)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var updatedUserResult = await _manageService.UpdateUserAsync(updatedUser);
        if (!updatedUserResult.IsSuccess)
            return BadRequest(updatedUserResult.ErrorMessage);

        return Ok(updatedUserResult.UpdatedId);
    }
    
    [HttpPost]
    [Route("client/create")]
    public async Task<IActionResult> AddClient(ManageClientRequest client)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addClientResult = await _manageService.AddClientAsync(client);
        if (!addClientResult.IsSuccess)
            return BadRequest(addClientResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("user/create")]
    public async Task<IActionResult> AddUser(ManageUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var userLogin = HttpContext.User.Identity.Name;
        var addUserResult = await _manageService.AddUserAsync(user, userLogin);
        if (!addUserResult.IsSuccess)
            return BadRequest(addUserResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("role/create")]
    public async Task<IActionResult> AddRole(ManageRoleRequest role)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addRoleResult = await _manageService.AddRoleAsync(role);
        if (!addRoleResult.IsSuccess)
            return BadRequest(addRoleResult.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("client/delete")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var deletedClient = await _manageService.DeleteClientAsync(id);
        if (!deletedClient.IsSuccess)
            return BadRequest(deletedClient.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("user/delete")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deletedUser = await _manageService.DeleteUserAsync(id);
        if (!deletedUser.IsSuccess)
            return BadRequest(deletedUser.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("role/delete")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var deletedRole = await _manageService.DeleteRoleAsync(id);
        if (!deletedRole.IsSuccess)
            return BadRequest(deletedRole.ErrorMessage);

        return Ok();
    }
}