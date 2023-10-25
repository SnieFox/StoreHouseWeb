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
    
    [HttpGet]
    [Route("suppliers")]
    public async Task<IActionResult> GetSuppliers()
    {
        var suppliers = await _storageService.GetAllSuppliersAsync();
        if (!suppliers.IsSuccess)
            return BadRequest(suppliers.ErrorMessage);
        
        return Ok(suppliers.AllSuppliers);
    }
    
    [HttpGet]
    [Route("writeoffcauses")]
    public async Task<IActionResult> GetWriteOffCauses()
    {
        var writeOffCauses = await _storageService.GetAllWriteOffCausesAsync();
        if (!writeOffCauses.IsSuccess)
            return BadRequest(writeOffCauses.ErrorMessage);
        
        return Ok(writeOffCauses.AllWriteOffCauses);
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
    
    [HttpPut]
    [Route("writeoff/update")]
    public async Task<IActionResult> UpdateWriteOff(StorageWriteOffRequest updatedWriteOff)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var updatedWriteOffResult = await _storageService.UpdateWriteOffAsync(updatedWriteOff);
        if (!updatedWriteOffResult.IsSuccess)
            return BadRequest(updatedWriteOffResult.ErrorMessage);

        return Ok(updatedWriteOffResult.UpdatedId);
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
    
    [HttpPost]
    [Route("supplier/create")]
    public async Task<IActionResult> AddSupplier(StorageSupplierRequest addSupplier)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addSupplierResult = await _storageService.AddSupplierAsync(addSupplier);
        if (!addSupplierResult.IsSuccess)
            return BadRequest(addSupplierResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("writeoffcause/create")]
    public async Task<IActionResult> AddWriteOffCause(StorageWriteOffCauseRequest addWriteOffCause)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addWriteOffCauseResult = await _storageService.AddWriteOffCauseAsync(addWriteOffCause);
        if (!addWriteOffCauseResult.IsSuccess)
            return BadRequest(addWriteOffCauseResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("writeoff/create")]
    public async Task<IActionResult> AddWriteOff(StorageWriteOffRequest updatedWriteOff)
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

        var addWriteOffResult = await _storageService.AddWriteOffAsync(updatedWriteOff, userLogin);
        if (!addWriteOffResult.IsSuccess)
            return BadRequest(addWriteOffResult.ErrorMessage);

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
    
    [HttpDelete]
    [Route("supplier/delete")]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var deletedSupplier = await _storageService.DeleteSupplierAsync(id);
        if (!deletedSupplier.IsSuccess)
            return BadRequest(deletedSupplier.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("writeoffcause/delete")]
    public async Task<IActionResult> DeleteWriteOffCause(int id)
    {
        var deletedWriteOffCause = await _storageService.DeleteWriteOffCauseAsync(id);
        if (!deletedWriteOffCause.IsSuccess)
            return BadRequest(deletedWriteOffCause.ErrorMessage);

        return Ok();
    }
}