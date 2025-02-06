using MediatR;

namespace Application.Features.TokenManagement.GetUserRoleFromToken
{
    public record GetUserRoleFromTokenQuery : IRequest<string>;
}
