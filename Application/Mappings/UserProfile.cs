using Application.Features.Authentication.Dtos;
using Application.Features.UserManagement.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationDto, User>()
                .ForMember(dest => dest.ChildrenCount, opt => opt.MapFrom(src => src.HasChildren ? src.ChildrenCount : 0));
            CreateMap<User, UserDto>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
