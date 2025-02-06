using Application.Features.UserManagement.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.UserManagement.Queries.SearchUsers
{
    public record SearchUsersQuery(string searchQuery, int pageNumber, int pageSize): IRequest<PaginatedResponseModel<UserDto>>;
}
