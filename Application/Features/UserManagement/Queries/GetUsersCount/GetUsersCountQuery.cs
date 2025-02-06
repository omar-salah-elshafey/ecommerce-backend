using MediatR;

namespace Application.Features.UserManagement.Queries.GetUsersCount
{
    public record GetUsersCountQuery : IRequest<int>;
}
