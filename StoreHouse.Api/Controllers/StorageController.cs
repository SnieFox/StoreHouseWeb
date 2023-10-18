using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse.Api.Model.DTO.StorageDTO;
using StoreHouse.Api.Services.Interfaces;

namespace StoreHouse.Api.Controllers;

[ApiController]
[Route($"storage")]
[Authorize]
public class StorageController : Controller
{
    private readonly IStorageService _storageService;
    public StorageController(IStorageService storageService) => _storageService = storageService;
    
    [HttpGet]
    [Route($"remains")]
    public async Task<IActionResult> GetAllRemains()
    {
        var remains = await _storageService.GetAllRemainsAsync();
        if (!remains.IsSuccess)
            return BadRequest(remains.ErrorMessage);
        
        return Ok(remains.AllRemains);
    }

    [HttpGet]
    [Route("supplies")]
    public async Task<IActionResult> GetAllSupplies()
    {
        var supplies = await _storageService.GetAllSuppliesAsync();
        if (!supplies.IsSuccess)
            return BadRequest(supplies.ErrorMessage);
        
        return Ok(supplies.AllSupplies);
    }
    
    [HttpGet]
    [Route("writeoffs")]
    public async Task<IActionResult> GetAllWriteOffs()
    {
        var writeOffs = await _storageService.GetAllWriteOffsAsync();
        if (!writeOffs.IsSuccess)
            return BadRequest(writeOffs.ErrorMessage);
        
        return Ok(writeOffs.AllWriteOffs);
    }
    
    [HttpPut]
    [Route("supply/update")]
    public async Task<IActionResult> UpdateSupply(StorageSupplyRequest updatedSupply)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var updatedSupplyResult = await _storageService.UpdateSupplyAsync(updatedSupply);
        if (!updatedSupplyResult.IsSuccess)
            return BadRequest(updatedSupplyResult.ErrorMessage);

        return Ok(updatedSupplyResult.UpdatedId);
    }

    [HttpPost]
    [Route("supply/create")]
    public async Task<IActionResult> AddSupply(StorageSupplyRequest updatedSupply)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        string userLogin = HttpContext.User.Identity.Name;

        var addSupplyResult = await _storageService.AddSupplyAsync(updatedSupply, userLogin);
        if (!addSupplyResult.IsSuccess)
            return BadRequest(addSupplyResult.ErrorMessage);

        return Ok();
    }

    [HttpDelete]
    [Route("supply/delete")]
    public async Task<IActionResult> DeleteSupply(int id)
    {
        var deletedSupply = await _storageService.DeleteSupplyAsync(id);
        if (!deletedSupply.IsSuccess)
            return BadRequest(deletedSupply.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("writeoff/delete")]
    public async Task<IActionResult> DeleteWriteOff(int id)
    {
        var deletedWriteOff = await _storageService.DeleteWriteOffAsync(id);
        if (!deletedWriteOff.IsSuccess)
            return BadRequest(deletedWriteOff.ErrorMessage);

        return Ok();
    }
}