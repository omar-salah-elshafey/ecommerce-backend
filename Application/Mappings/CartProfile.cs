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
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.Price))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.Images.FirstOrDefault(i => i.IsMain)!.ImageUrl))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Product.Stock))
                .ForMember(dest => dest.MaxOrderQuantity, opt => opt.MapFrom(src => src.Product.MaxOrderQuantity));
        }
    }
}
