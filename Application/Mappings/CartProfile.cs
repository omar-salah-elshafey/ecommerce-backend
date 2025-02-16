using Application.Features.Carts.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.TotalCartPrice,
                       opt => opt.MapFrom(src => src.Items.Sum(item => item.Quantity * item.Product.Price)));
            CreateMap<CartItem, CartItemsDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.Price));
        }
    }
}
