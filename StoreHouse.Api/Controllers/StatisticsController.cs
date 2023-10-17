using Microsoft.AspNetCore.Mvc;
using StoreHouse.Api.Services.StatisticsServices.StatisticsInterfaces;
using StoreHouse.Database.Entities;

namespace StoreHouse.Api.Controllers;

[ApiController]
[Route($"stats")]
public class StatisticsController : Controller
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService) => _statisticsService = statisticsService;
    
    [HttpGet]
    [Route("clients")]
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _statisticsService.GetAllClientsAsync();
        if (!clients.IsSuccess)
            return BadRequest(clients.ErrorMessage);

        return Ok(clients.AllClients);
    }
    
    [HttpGet]
    [Route("employees")]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _statisticsService.GetAllEmployeesAsync();
        if (!employees.IsSuccess)
            return BadRequest(employees.ErrorMessage);

        return Ok(employees.AllEmployee);
    }
    
    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _statisticsService.GetAllProductsAsync();
        if (!products.IsSuccess)
            return BadRequest(products.ErrorMessage);

        return Ok(products.AllProduct);
    }
    
    [HttpGet]
    [Route("receipts")]
    public async Task<IActionResult> GetAllReceipts()
    {
        var receipts = await _statisticsService.GetAllReceiptsAsync();
        if (!receipts.IsSuccess)
            return BadRequest(receipts.ErrorMessage);

        return Ok(receipts.AllReceipt);
    }
   
}