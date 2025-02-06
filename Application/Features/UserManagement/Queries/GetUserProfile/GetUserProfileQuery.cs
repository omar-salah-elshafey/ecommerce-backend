using Application.Features.UserManagement.Dtos;
using MediatR;

namespace Application.Features.UserManagement.Queries.GetUserProfile
{
    public record GetUserProfileQuery(string userName): IRequest<UserDto>;
}
