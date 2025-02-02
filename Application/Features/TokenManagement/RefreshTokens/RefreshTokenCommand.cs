using Application.Features.Authentication.Dtos;
using MediatR;

namespace Application.Features.TokenManagement.RefreshTokens
{
    public record RefreshTokenCommand(string token) : IRequest<AuthResponseModel>;
}
