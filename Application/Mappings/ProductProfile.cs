using Application.Features.Products.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Images,
                           opt => opt.MapFrom(src => src.Images ?? new List<ProductImage>()));
            
            CreateMap<ProductImage, ProductImageDto>();
        }
    }
}
