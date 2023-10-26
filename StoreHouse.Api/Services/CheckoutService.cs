using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreHouse.Api.Model.DTO.CheckoutDTO;
using StoreHouse.Api.Model.DTO.DishListDTO;
using StoreHouse.Api.Model.DTO.ProductListDTO;
using StoreHouse.Api.Services.Interfaces;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.Interfaces;

namespace StoreHouse.Api.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IProductCategoryService _productCategoryService;
    private readonly IClientService _clientService;
    private readonly IReceiptService _receiptService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public CheckoutService(IUserService userService, IReceiptService receiptService, IClientService clientService, IMapper mapper, IProductCategoryService productCategoryService)
    {
        _userService = userService;
        _receiptService = receiptService;
        _clientService = clientService;
        _mapper = mapper;
        _productCategoryService = productCategoryService;
    }
    public async Task<(bool IsSuccess, string ErrorMessage, List<CheckoutProductCategoryResponse> AllProductCategories)> GetAllProductCategoriesAsync()
    {
        try
        {
            //Get Product Categories
            var productCategories = await _productCategoryService.GetAllProductCategoriesAsync();
            if (!productCategories.IsSuccess)
                return (false, productCategories.ErrorMessage, new List<CheckoutProductCategoryResponse>());

            //Map Product Categories
            var productCategoriesMap = _mapper.Map<List<CheckoutProductCategoryResponse>>(productCategories);
            if (productCategoriesMap == null)
                return (false, "Mapping failed, object is null", new List<CheckoutProductCategoryResponse>());

            foreach (var categoryResponse in productCategoriesMap)
            {
                foreach (var product in productCategories.ProductCategoryList.Where(d => d.Id == categoryResponse.Id))
                {
                    if (product.Products.Count == 0)
                    {
                        var productListDishMap = _mapper.Map<DishListResponse>(product.Dishes);
                        if (productListDishMap == null)
                            return (false, "Mapping failed, object is null",
                                new List<CheckoutProductCategoryResponse>());
                        categoryResponse.DishList.Add(productListDishMap);
                    }
                    else if (product.Dishes.Count == 0)
                    {
                        var productListProdMap = _mapper.Map<DishListResponse>(product.Products);
                        if (productListProdMap == null)
                            return (false, "Mapping failed, object is null",
                                new List<CheckoutProductCategoryResponse>());
                        categoryResponse.DishList.Add(productListProdMap);
                    }
                    else
                    {
                        var productListDishMap = _mapper.Map<DishListResponse>(product.Dishes);
                        if (productListDishMap == null)
                            return (false, "Mapping failed, object is null",
                                new List<CheckoutProductCategoryResponse>());
                        var productListProdMap = _mapper.Map<DishListResponse>(product.Products);
                        if (productListProdMap == null)
                            return (false, "Mapping failed, object is null",
                                new List<CheckoutProductCategoryResponse>());
                        categoryResponse.DishList.Add(productListProdMap);
                        categoryResponse.DishList.Add(productListDishMap);
                    }
                }
            }

            return (true, string.Empty, productCategoriesMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<CheckoutProductCategoryResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<CheckoutClientResponse> AllClients)> GetAllClientsAsync()
    {
        try
        {
            //Get Clients
            var clients = await _clientService.GetAllClientsAsync();
            if (!clients.IsSuccess)
                return (false, clients.ErrorMessage, new List<CheckoutClientResponse>());

            //Map Clients
            var clientsMap = _mapper.Map<List<CheckoutClientResponse>>(clients);
            if (clientsMap == null)
                return (false, "Mapping failed, object is null", new List<CheckoutClientResponse>());

            //Change required Data
            foreach (var clientResponse in clientsMap)
            {
                foreach (var client in clients.ClientList.Where(wr => wr.Id == clientResponse.Id))
                {
                    decimal receiptsSum = 0;
                    foreach (var receipt in client.Receipts)
                    {
                        var sum = GetSum(receipt.ProductLists);
                        if (!sum.IsSuccess)
                            return (false, sum.ErrorMessage, new List<CheckoutClientResponse>());

                        receiptsSum += sum.Sum;
                    }

                    clientResponse.ReceiptSum = receiptsSum;
                }
            }

            return (true, string.Empty, clientsMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<CheckoutClientResponse>());
        }

    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddClientAsync(CheckoutClientRequest client)
    {
        try
        {
            //Map Clients
            var clientMap = _mapper.Map<Client>(client);
            if (clientMap == null)
                return (false, "Mapping failed, object was null");

            //Call the DAL update service
            var addClient = await _clientService.CreateClientAsync(clientMap);
            if (!addClient.IsSuccess)
                return (false, addClient.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage, List<CheckoutReceiptResponse> AllReceipts)> GetAllReceiptsAsync()
    {
        try
        {
            //Get All Receipts
            var receipts = await _receiptService.GetAllReceiptsAsync();
            if (!receipts.IsSuccess) return (false, receipts.ErrorMessage, new List<CheckoutReceiptResponse>());

            //Mapping Receipts to StatisticsReceiptResponse
            var receiptsMap = _mapper.Map<List<CheckoutReceiptResponse>>(receipts);
            if (receiptsMap == null)
                return (false, "Mapping failed, object is null", new List<CheckoutReceiptResponse>());

            //Change required fields
            foreach (var receiptResponse in receiptsMap)
            {
                foreach (var receipt in receipts.ReceiptList.Where(receipt => receipt.Id == receiptResponse.Id))
                {
                    var productList = _mapper.Map<List<ReceiptProductListResponse>>(receipt.ProductLists);
                    if (productList == null)
                        return (false, "Mapping failed, object is null", new List<CheckoutReceiptResponse>());

                    receiptResponse.ProductList = productList;
                }
            }

            return (true, string.Empty, receiptsMap);
        }
        catch (Exception e)
        {
            return (false, e.Message, new List<CheckoutReceiptResponse>());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> AddReceiptAsync(CheckoutReceiptRequest receipt, string userLogin)
    {
        try
        {
            //Map Receipt
            var receiptMap = _mapper.Map<Receipt>(receipt);
            if (receiptMap == null)
                return (false, "Mapping failed, object was null");

            var productList = _mapper.Map<List<ProductList>>(receipt.ProductList);
            if (productList == null)
                return (false, "Mapping failed, object is null");

            //Get User by Login
            var user = await _userService.GetUserByLogin(userLogin);
            if (!user.IsSuccess)
                return (false, user.ErrorMessage);

            receiptMap.ProductLists = productList;
            receiptMap.UserId = user.User.Id;

            //Call the DAL update service
            var addReceipt = await _receiptService.CreateReceiptAsync(receiptMap);
            if (!addReceipt.IsSuccess)
                return (false, addReceipt.ErrorMessage);

            return (true, string.Empty);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
    
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