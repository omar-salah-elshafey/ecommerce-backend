using Application.Features.UsersMessages.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class UsersMessagesProfile : Profile
    {
        public UsersMessagesProfile()
        {
            CreateMap<UsersMessage, MessageDto>();
        }
    }
}
