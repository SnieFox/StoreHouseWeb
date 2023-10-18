using AutoMapper;
using StoreHouse.Api.Model.DTO.StatisticsDTO;
using StoreHouse.Api.Services.Interfaces;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;

namespace StoreHouse.Api.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IClientService _clientService;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly IReceiptService _receiptService;
    private readonly IMapper _mapper;

    public StatisticsService(IClientService clientService, IUserService userService, IProductService productService, IReceiptService receiptService, IMapper mapper)
    {
        _mapper = mapper;
        _receiptService = receiptService;
        _productService = productService;
        _userService = userService;
        _clientService = clientService;
    }
    
    public async Task<(bool IsSuccess, string ErrorMessage, List<StatisticsClientResponse> AllClients)> GetAllClientsAsync()
    {
        try
        {
            //Get all clients
            var clients = await _clientService.GetAllClientsAsync();
            if (!clients.IsSuccess)
                return (false, clients.ErrorMessage, new List<StatisticsClientResponse>());

            //AutoMap Clients to StatisticsClientResponse
            var allClientsResponse = _mapper.Map<List<StatisticsClientResponse>>(clients);
            if (allClientsResponse == null)
                return (false, "Mapping failed, object is null", new List<StatisticsClientResponse>());

            //Change required fields
            foreach (var clientResponse in allClientsResponse)
            {
                foreach (var client in clients.ClientList.Where(client => client.Id == clientResponse.Id))
                {
                    if (client.Receipts.Count == 0)
                    {
                        clientResponse.ByCardSum = 0;
                        clientResponse.ByCashSum = 0;
                        clientResponse.AverageReceiptSum = 0;
                        clientResponse.ReceiptsCount = client.Receipts.Count;
                    }
                    else
                    {
                        var sum = GetSum(client.Receipts);
                        if (!sum.IsSuccess)
                            return (false, sum.ErrorMessage, new List<StatisticsClientResponse>());
                        
                        clientResponse.ByCardSum = sum.byCard;
                        clientResponse.ByCashSum = sum.byCash;
                        clientResponse.AverageReceiptSum = sum.averageSum;
                        clientResponse.ReceiptsCount = client.Receipts.Count;
                    }
                }
            }

            return (true, string.Empty, allClientsResponse);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<StatisticsClientResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<StatisticsEmployeeResponse> AllEmployee)> GetAllEmployeesAsync()
    {
        try
        {
            //Get all employees
            var employees = await _userService.GetAllUsersAsync();
            if (!employees.IsSuccess) return (false, employees.ErrorMessage, new List<StatisticsEmployeeResponse>());

            //AutoMap Employees to StatisticsEmployeeResponse
            var allEmployeesResponse = _mapper.Map<List<StatisticsEmployeeResponse>>(employees);
            if (allEmployeesResponse == null)
                return (false, "Mapping failed, object is null", new List<StatisticsEmployeeResponse>());

            //Change required fields
            foreach (var employeeResponse in allEmployeesResponse)
            {
                foreach (var employee in employees.UserList.Where(employee => employee.Id == employeeResponse.Id))
                {
                    if (employee.Receipts.Count == 0)
                    {
                        employeeResponse.ReceiptSum = 0;
                        employeeResponse.AverageReceiptSum = 0;
                        employeeResponse.ReceiptsCount = employee.Receipts.Count;
                    }
                    else
                    {
                        var sum = GetSum(employee.Receipts);
                        if (!sum.IsSuccess)
                            return (false, sum.ErrorMessage, new List<StatisticsEmployeeResponse>());

                        employeeResponse.ReceiptSum = sum.byCard + sum.byCash;
                        employeeResponse.AverageReceiptSum = sum.averageSum;
                        employeeResponse.ReceiptsCount = employee.Receipts.Count;
                    }
                }
            }
            
            return (true, string.Empty, allEmployeesResponse);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<StatisticsEmployeeResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<StatisticsProductResponse> AllProduct)> GetAllProductsAsync()
    {
        try
        {
            //Get all Products
            var products = await _productService.GetAllProductsAsync();
            if (!products.IsSuccess) return (false, products.ErrorMessage, new List<StatisticsProductResponse>());

            //Mappping Products to StatisticsProductResponse
            var allProductsResponse = _mapper.Map<List<StatisticsProductResponse>>(products);
            if (allProductsResponse == null)
                return (false, "Mapping failed, object is null", new List<StatisticsProductResponse>());

            //Change required fields
            foreach (var productResponse in allProductsResponse)
            {
                foreach (var product in products.ProductList.Where(product => product.Id == productResponse.Id))
                {
                    var productList = await _productService.GetProductListByNameAsync(product.Name);
                    if (!productList.IsSuccess)
                        return (false, productList.ErrorMessage, new List<StatisticsProductResponse>());

                    var sum = GetSum(productList.ProductList);
                    if (!sum.IsSuccess)
                        return (false, sum.ErrorMessage, new List<StatisticsProductResponse>());

                    productResponse.SoldCount = productList.ProductList.Count;
                    productResponse.Sum = sum.Sum;
                }
            }

            return (true, string.Empty, allProductsResponse);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<StatisticsProductResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<StatisticsReceiptResponse> AllReceipt)> GetAllReceiptsAsync()
    {
        try
        {
            //Get All Receipts
            var receipts = await _receiptService.GetAllReceiptsAsync();
            if (!receipts.IsSuccess) return (false, receipts.ErrorMessage, new List<StatisticsReceiptResponse>());

            //Mapping Receipts to StatisticsReceiptResponse
            var allReceiptsResponse = _mapper.Map<List<StatisticsReceiptResponse>>(receipts);
            if (allReceiptsResponse == null)
                return (false, "Mapping failed, object is null", new List<StatisticsReceiptResponse>());

            //Change required fields
            foreach (var receiptResponse in allReceiptsResponse)
            {
                foreach (var receipt in receipts.ReceiptList.Where(receipt => receipt.Id == receiptResponse.Id))
                {
                    var sum = GetSum(receipt.ProductLists);
                    if (!sum.IsSuccess)
                        return (false, sum.ErrorMessage, new List<StatisticsReceiptResponse>());
                    
                    receiptResponse.Sum = sum.Sum;
                }
            }

            return (true, string.Empty, allReceiptsResponse);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<StatisticsReceiptResponse>());
        }
    }

    
    //Static method to Get SumByCard, SumByCash and AverageReceiptSum for separate Client
    private static (bool IsSuccess, string ErrorMessage, decimal byCard, decimal byCash, decimal averageSum) GetSum(List<Receipt> receipts)
    {
        try
        {
            decimal sumByCard = 0;
            decimal sumByCash = 0;

            foreach (var receipt in receipts)
            {
                if (receipt.Type == "Карта")
                {
                    foreach (var product in receipt.ProductLists)
                    {
                        sumByCard += (decimal)product.Count * product.Price;
                    }
                }
                else if (receipt.Type == "Готівка")
                {
                    foreach (var product in receipt.ProductLists)
                    {
                        sumByCash += (decimal)product.Count * product.Price;
                    }
                }
            }

            var averageReceiptSum = (sumByCard + sumByCash) / receipts.Count;

            return (true, string.Empty, sumByCard, sumByCash, averageReceiptSum);
        }
        catch (Exception e)
        {
            return (false, e.Message, 0, 0, 0);
        }
    }

    //Static method to Get Sum of products in ProductLists
    private static (bool IsSuccess, string ErrorMessage, decimal Sum) GetSum(List<ProductList> productLists)
    {
        try
        {
            decimal sum = 0;

            foreach (var product in productLists)
            {
                sum += product.Price * (decimal)product.Count;
            }

            return (true, string.Empty, sum);
        }
        catch (Exception e)
        {
            return (false, e.Message, 0);
        }
    }
}