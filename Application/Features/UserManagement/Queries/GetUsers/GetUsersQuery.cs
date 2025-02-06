using Application.Features.UserManagement.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.UserManagement.Queries.GetUsers
{
    public record GetUsersQuery(int pageNumber, int pageSize) : IRequest<PaginatedResponseModel<UserDto>>;
}
