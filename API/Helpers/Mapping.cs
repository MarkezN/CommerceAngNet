using API.Controllers;
using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
    }
}