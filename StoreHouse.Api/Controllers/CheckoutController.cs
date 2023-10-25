using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse.Api.Model.DTO.CheckoutDTO;
using StoreHouse.Api.Services.Interfaces;

namespace StoreHouse.Api.Controllers;

[ApiController]
[Route($"checkout")]
[Authorize]
public class CheckoutController : Controller
{
    private readonly ICheckoutService _checkoutService;
    public CheckoutController(ICheckoutService checkoutService) => _checkoutService = checkoutService;
    
    [HttpGet]
    [Route($"products")]
    public async Task<IActionResult> GetAllProductCategories()
    {
        var productCategories = await _checkoutService.GetAllProductCategoriesAsync();
        if (!productCategories.IsSuccess)
            return BadRequest(productCategories.ErrorMessage);
        
        return Ok(productCategories.AllProductCategories);
    }
    
    [HttpGet]
    [Route($"clients")]
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _checkoutService.GetAllClientsAsync();
        if (!clients.IsSuccess)
            return BadRequest(clients.ErrorMessage);
        
        return Ok(clients.AllClients);
    }
    
    [HttpGet]
    [Route($"receipts")]
    public async Task<IActionResult> GetAllReceipts()
    {
        var receipts = await _checkoutService.GetAllReceiptsAsync();
        if (!receipts.IsSuccess)
            return BadRequest(receipts.ErrorMessage);
        
        return Ok(receipts.AllReceipts);
    }
    
    [HttpPost]
    [Route("clients/create")]
    public async Task<IActionResult> AddClient(CheckoutClientRequest client)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addClientResult = await _checkoutService.AddClientAsync(client);
        if (!addClientResult.IsSuccess)
            return BadRequest(addClientResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("receipts/create")]
    public async Task<IActionResult> AddReceipt(CheckoutReceiptRequest receipt)
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

        var addReceiptResult = await _checkoutService.AddReceiptAsync(receipt, userLogin);
        if (!addReceiptResult.IsSuccess)
            return BadRequest(addReceiptResult.ErrorMessage);

        return Ok();
    }

}