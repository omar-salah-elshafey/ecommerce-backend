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
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Governorate, GovernorateDto>();

            CreateMap<City, CityDto>();
        }
    }
}
