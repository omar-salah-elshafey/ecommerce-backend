using Domain.Entities;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.TokenManagement.CreateToken
{
    public record CreateTokenCommand(User user) : IRequest<JwtSecurityToken>;
}
