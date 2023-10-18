﻿using AutoMapper;
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
        

        #endregion

    }
}