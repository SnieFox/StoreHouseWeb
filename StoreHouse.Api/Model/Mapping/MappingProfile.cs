using AutoMapper;
using StoreHouse.Api.Model.DTO.StatisticsDTO;
using StoreHouse.Database.Entities;

namespace StoreHouse.Api.Model.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
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
        
    }
}