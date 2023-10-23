using System.Runtime.CompilerServices;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using StoreHouse.Api.Model.DTO.CheckoutDTO;
using StoreHouse.Api.Model.DTO.DishListDTO;
using StoreHouse.Api.Model.DTO.ManageDTO;
using StoreHouse.Api.Model.DTO.MenuDTO;
using StoreHouse.Api.Model.DTO.ProductListDTO;
using StoreHouse.Api.Model.DTO.StatisticsDTO;
using StoreHouse.Api.Model.DTO.StorageDTO;
using StoreHouse.Database.Entities;

namespace StoreHouse.Api.Model.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Mapping Data for Statistics Service
        
        this.CreateMap<Client, StatisticsClientResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.FullName,
                op => op.MapFrom(srs => srs.FullName))
            .ForMember(d => d.MobilePhone,
                op => op.MapFrom(srs => srs.MobilePhone));
        
        this.CreateMap<User, StatisticsEmployeeResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.FullName,
                op => op.MapFrom(srs => srs.FullName));
        
        this.CreateMap<Product, StatisticsProductResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name));
        
        this.CreateMap<Receipt, StatisticsReceiptResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.UserName,
                op => op.MapFrom(srs => srs.UserName))
            .ForMember(d => d.Type,
                op => op.MapFrom(srs => srs.Type));
        
        #endregion

        #region Mapping Data for Storage Service

        this.CreateMap<Product, StorageRemainResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.PrimeCost,
                op => op.MapFrom(srs => srs.PrimeCost))
            .ForMember(d => d.Remains,
                op => op.MapFrom(srs => srs.Remains))
            .ForMember(d => d.CategoryName,
                op => op.MapFrom(srs => srs.Category.Name));

        this.CreateMap<Ingredient, StorageRemainResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.PrimeCost,
                op => op.MapFrom(srs => srs.PrimeCost))
            .ForMember(d => d.Remains,
                op => op.MapFrom(srs => srs.Remains))
            .ForMember(d => d.CategoryName,
                op => op.MapFrom(srs => srs.Category.Name));

        this.CreateMap<Supply, StorageSupplyResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Date,
                op => op.MapFrom(srs => srs.Date))
            .ForMember(d => d.SupplierName,
                op => op.MapFrom(srs => srs.Supplier.Name));

        this.CreateMap<ProductList, SupplyProductListResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Count,
                op => op.MapFrom(srs => srs.Count))
            .ForMember(d => d.Sum,
                op => op.MapFrom(srs => srs.Price));

        this.CreateMap<StorageSupplyRequest, Supply>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Date,
                op => op.MapFrom(srs => srs.Date))
            .ForMember(d => d.Comment,
                op => op.MapFrom(srs => srs.Comment));

        this.CreateMap<SupplyProductListRequest, ProductList>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Count,
                op => op.MapFrom(srs => srs.Count));

        this.CreateMap<WriteOff, StorageWriteOffResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Date,
                op => op.MapFrom(srs => srs.Date))
            .ForMember(d => d.UserName,
                op => op.MapFrom(srs => srs.UserName))
            .ForMember(d => d.Cause,
                op => op.MapFrom(srs => srs.Cause.Name));

        this.CreateMap<ProductList, WriteOffProductListResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Count,
                op => op.MapFrom(srs => srs.Count))
            .ForMember(d => d.Comment,
                op => op.MapFrom(srs => srs.Comment))
            .ForMember(d => d.Sum,
                op => op.MapFrom(srs => srs.Price));

        this.CreateMap<StorageWriteOffRequest, WriteOff>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Date,
                op => op.MapFrom(srs => srs.Date))
            .ForMember(d => d.Comment,
                op => op.MapFrom(srs => srs.Comment));

        this.CreateMap<WriteOffProductListRequest, ProductList>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Comment,
                op => op.MapFrom(srs => srs.Comment))
            .ForMember(d => d.Count,
                op => op.MapFrom(srs => srs.Count));

        #endregion

        #region Mapping Data for Menu Service

        this.CreateMap<Product, MenuProductResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.ImageId,
                op => op.MapFrom(srs => srs.ImageId))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Category,
                op => op.MapFrom(srs => srs.Category.Name))
            .ForMember(d => d.PrimeCost,
                op => op.MapFrom(srs => srs.PrimeCost))
            .ForMember(d => d.Price,
                op => op.MapFrom(srs => srs.Price));

        this.CreateMap<MenuProductRequest, Product>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.ImageId,
                op => op.MapFrom(srs => srs.ImageId))
            .ForMember(d => d.PrimeCost,
                op => op.MapFrom(srs => srs.PrimeCost))
            .ForMember(d => d.Price,
                op => op.MapFrom(srs => srs.Price));

        this.CreateMap<Dish, MenuDishResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.CategoryName,
                op => op.MapFrom(srs => srs.Category.Name))
            .ForMember(d => d.Price,
                op => op.MapFrom(srs => srs.Price))
            .ForMember(d => d.ImageId,
                op => op.MapFrom(srs => srs.ImageId));

        this.CreateMap<ProductList, MenuProductListResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Weight,
                op => op.MapFrom(srs => srs.Count))
            .ForMember(d => d.PrimeCost,
                op => op.MapFrom(srs => srs.PrimeCost));

        this.CreateMap<MenuDishRequest, Dish>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Price,
                op => op.MapFrom(srs => srs.Price))
            .ForMember(d => d.ImageId,
                op => op.MapFrom(srs => srs.ImageId));

        this.CreateMap<MenuProductListRequest, ProductList>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Count,
                op => op.MapFrom(srs => srs.Weight));

        this.CreateMap<SemiProduct, MenuSemiProductResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Weight,
                op => op.MapFrom(srs => srs.Output))
            .ForMember(d => d.PrimeCost,
                op => op.MapFrom(srs => srs.PrimeCost))
            .ForMember(d => d.Comment,
                op => op.MapFrom(srs => srs.Prescription));

        this.CreateMap<MenuSemiProductRequest, SemiProduct>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Prescription,
                op => op.MapFrom(srs => srs.Comment));

        this.CreateMap<Ingredient, MenuIngredientResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.CategoryName,
                op => op.MapFrom(srs => srs.Category.Name))
            .ForMember(d => d.Unit,
                op => op.MapFrom(srs => srs.Unit))
            .ForMember(d => d.Remains,
                op => op.MapFrom(srs => srs.Remains))
            .ForMember(d => d.PrimeCost,
                op => op.MapFrom(srs => srs.PrimeCost));

        this.CreateMap<MenuIngredientUpdateRequest, Ingredient>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name));

        this.CreateMap<MenuIngredientAddRequest, Ingredient>()
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Unit,
                op => op.MapFrom(srs => srs.Unit))
            .ForMember(d => d.Remains,
                op => op.MapFrom(srs => srs.Remains))
            .ForMember(d => d.PrimeCost,
                op => op.MapFrom(srs => srs.PrimeCost));

        this.CreateMap<ProductCategory, MenuProductCategoryResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.ImageId,
                op => op.MapFrom(srs => srs.ImageId));

        this.CreateMap<MenuProductCategoryRequest, ProductCategory>()
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.ImageId,
                op => op.MapFrom(srs => srs.ImageId));

        this.CreateMap<IngredientsCategory, MenuIngredientCategoryResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name));

        this.CreateMap<MenuIngredientCategoryRequest, IngredientsCategory>()
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name));

        #endregion

        #region Mapping Data for Manage Service

        this.CreateMap<Client, ManageClientResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.FullName,
                op => op.MapFrom(srs => srs.FullName))
            .ForMember(d => d.BankCard,
                op => op.MapFrom(srs => srs.BankCard))
            .ForMember(d => d.MobilePhone,
                op => op.MapFrom(srs => srs.MobilePhone));

        this.CreateMap<ManageClientRequest, Client>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.FullName,
                op => op.MapFrom(srs => srs.FullName))
            .ForMember(d => d.BankCard,
                op => op.MapFrom(srs => srs.BankCard))
            .ForMember(d => d.MobilePhone,
                op => op.MapFrom(srs => srs.MobilePhone))
            .ForMember(d => d.Email,
                op => op.MapFrom(srs => srs.Email))
            .ForMember(d => d.Comment,
                op => op.MapFrom(srs => srs.Comment))
            .ForMember(d => d.Address,
                op => op.MapFrom(srs => srs.Address));

        this.CreateMap<User, ManageUserResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.FullName))
            .ForMember(d => d.Login,
                op => op.MapFrom(srs => srs.Login))
            .ForMember(d => d.PinCode,
                op => op.MapFrom(srs => srs.PinCode))
            .ForMember(d => d.RoleName,
                op => op.MapFrom(srs => srs.Role.Name))
            .ForMember(d => d.LastLoginDate,
                op => op.MapFrom(srs => srs.LastLoginDate));

        this.CreateMap<ManageUserRequest, User>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.FullName,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Login,
                op => op.MapFrom(srs => srs.Login))
            .ForMember(d => d.Email,
                op => op.MapFrom(srs => srs.Email))
            .ForMember(d => d.MobilePhone,
                op => op.MapFrom(srs => srs.MobilePhone))
            .ForMember(d => d.PinCode,
                op => op.MapFrom(srs => srs.PinCode));

        this.CreateMap<Role, ManageRoleResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name));

        this.CreateMap<ManageRoleRequest, Role>()
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name));

        #endregion

        #region MyRegion

        this.CreateMap<ProductCategory, CheckoutProductCategoryResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.ImageId,
                op => op.MapFrom(srs => srs.ImageId));

        this.CreateMap<Dish, DishListResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.ImageId,
                op => op.MapFrom(srs => srs.ImageId))
            .ForMember(d => d.Price,
                op => op.MapFrom(srs => srs.Price));

        this.CreateMap<Product, DishListResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Price,
                op => op.MapFrom(srs => srs.Price))
            .ForMember(d => d.ImageId,
                op => op.MapFrom(srs => srs.ImageId));

        this.CreateMap<Client, CheckoutClientResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.FullName,
                op => op.MapFrom(srs => srs.FullName))
            .ForMember(d => d.Sex,
                op => op.MapFrom(srs => srs.Sex))
            .ForMember(d => d.BankCard,
                op => op.MapFrom(srs => srs.BankCard))
            .ForMember(d => d.MobilePhone,
                op => op.MapFrom(srs => srs.MobilePhone));

        this.CreateMap<CheckoutClientRequest, Client>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.FullName,
                op => op.MapFrom(srs => srs.FullName))
            .ForMember(d => d.Sex,
                op => op.MapFrom(srs => srs.Sex))
            .ForMember(d => d.BirthDate,
                op => op.MapFrom(srs => srs.BirthDate))
            .ForMember(d => d.BankCard,
                op => op.MapFrom(srs => srs.BankCard))
            .ForMember(d => d.MobilePhone,
                op => op.MapFrom(srs => srs.MobilePhone))
            .ForMember(d => d.Email,
                op => op.MapFrom(srs => srs.Email))
            .ForMember(d => d.Comment,
                op => op.MapFrom(srs => srs.Comment))
            .ForMember(d => d.Address,
                op => op.MapFrom(srs => srs.Address));

        this.CreateMap<Receipt, CheckoutReceiptResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Type,
                op => op.MapFrom(srs => srs.Type))
            .ForMember(d => d.UserName,
                op => op.MapFrom(srs => srs.UserName))
            .ForMember(d => d.ClientName,
                op => op.MapFrom(srs => srs.ClientName))
            .ForMember(d => d.OpenDate,
                op => op.MapFrom(srs => srs.OpenDate))
            .ForMember(d => d.CloseDate,
                op => op.MapFrom(srs => srs.CloseDate));

        this.CreateMap<ProductList, ReceiptProductListResponse>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Count,
                op => op.MapFrom(srs => srs.Count))
            .ForMember(d => d.Price,
                op => op.MapFrom(srs => srs.Price));

        this.CreateMap<CheckoutReceiptRequest, Receipt>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Type,
                op => op.MapFrom(srs => srs.Type))
            .ForMember(d => d.UserName,
                op => op.MapFrom(srs => srs.UserName))
            .ForMember(d => d.ClientName,
                op => op.MapFrom(srs => srs.ClientName))
            .ForMember(d => d.OpenDate,
                op => op.MapFrom(srs => srs.OpenDate))
            .ForMember(d => d.CloseDate,
                op => op.MapFrom(srs => srs.CloseDate))
            .ForMember(d => d.ClientId,
                op => op.MapFrom(srs => srs.ClientId));

        this.CreateMap<ReceiptProductListRequest, ProductList>()
            .ForMember(d => d.Id,
                op => op.MapFrom(srs => srs.Id))
            .ForMember(d => d.Name,
                op => op.MapFrom(srs => srs.Name))
            .ForMember(d => d.Count,
                op => op.MapFrom(srs => srs.Count))
            .ForMember(d => d.Price,
                op => op.MapFrom(srs => srs.Price));

        #endregion
    }
}