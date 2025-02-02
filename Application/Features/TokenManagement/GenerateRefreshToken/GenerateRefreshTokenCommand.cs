using Domain.Entities;
using MediatR;

namespace Application.Features.TokenManagement.GenerateRefreshToken
{
    public record GenerateRefreshTokenCommand : IRequest<RefreshToken>;
}
