using MediatR;

namespace Application.Features.TokenManagement.GetUserIdFromToken
{
    public record GetUserIdFromTokenQuery : IRequest<string>;
}
