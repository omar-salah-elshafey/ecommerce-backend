using Application.Features.Wishlists.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class WishlistProfile : Profile
    {
        public WishlistProfile()
        {
            CreateMap<Wishlist, WishlistDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.WishlistItems));
            CreateMap<WishlistItem, WishlistItemDto>()
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Product.Stock))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.Images.FirstOrDefault(i => i.IsMain)!.ImageUrl));
        }
    }
}
