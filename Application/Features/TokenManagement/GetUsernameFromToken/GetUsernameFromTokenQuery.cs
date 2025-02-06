using MediatR;

namespace Application.Features.TokenManagement.GetUsernameFromToken
{
    public record GetUsernameFromTokenQuery : IRequest<string>;
}
