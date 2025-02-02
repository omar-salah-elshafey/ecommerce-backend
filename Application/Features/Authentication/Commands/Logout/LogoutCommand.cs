using MediatR;

namespace Application.Features.Authentication.Commands.Logout
{
    public record LogoutCommand(string refreshToken) : IRequest;
}
