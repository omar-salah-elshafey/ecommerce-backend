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
            CreateMap<WishlistItem, WishlistItemDto>();
        }
    }
}
