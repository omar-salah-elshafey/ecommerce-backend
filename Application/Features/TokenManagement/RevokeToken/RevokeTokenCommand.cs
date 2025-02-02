using MediatR;

namespace Application.Features.TokenManagement.RevokeToken
{
    public record RevokeTokenCommand(string token) : IRequest;
}
