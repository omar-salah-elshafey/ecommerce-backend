using Application.Features.Orders.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Governorate, opt => opt.MapFrom(src => src.Address.Governorate))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Address.Region));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.Images.FirstOrDefault(i => i.IsMain)!.ImageUrl));

            CreateMap<Governorate, GovernorateDto>();

            CreateMap<City, CityDto>();

            CreateMap<Address, AddressDto>();
        }
    }
}
