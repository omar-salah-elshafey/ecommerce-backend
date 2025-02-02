using Application.Features.Authentication.Dtos;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateJwtTokenAsync(User user);
        Task<AuthResponseModel> RefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(string token);
        Task<RefreshToken> GenerateRefreshToken();
    }
}
