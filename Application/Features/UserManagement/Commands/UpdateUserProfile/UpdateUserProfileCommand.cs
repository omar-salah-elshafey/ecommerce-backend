using Application.Features.UserManagement.Dtos;
using MediatR;

namespace Application.Features.UserManagement.Commands.UpdateUserProfile
{
    public record UpdateUserProfileCommand(string UserName, UpdateUserDto UpdateUserDto) : IRequest<UserDto>;
}
