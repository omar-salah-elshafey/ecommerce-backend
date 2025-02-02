using Application.ExceptionHandling;
using Application.Features.Authentication.Dtos;
using Application.Features.TokenManagement.CreateToken;
using Application.Features.TokenManagement.GenerateRefreshToken;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.TokenManagement.RefreshTokens
{
    public class RefreshTokenCommandHandler(UserManager<User> _userManager, ILogger<RefreshTokenCommandHandler> _logger,
        IMediator _mediator) : IRequestHandler<RefreshTokenCommand, AuthResponseModel>

    {
        public async Task<AuthResponseModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = request.token;
            _logger.LogInformation("Attempting to refresh token for token: {Token}", token);
            var authResponseModel = new AuthResponseModel();
            token = Uri.UnescapeDataString(token);
            var user = await _userManager.Users.Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
                throw new InvalidTokenException("Invalid Token! user null");
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
                throw new InvalidTokenException("Invalid Token! inactive token");
            
            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = await _mediator.Send(new GenerateRefreshTokenCommand());
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await _mediator.Send(new CreateTokenCommand(user));
            authResponseModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authResponseModel.ExpiresAt = jwtToken.ValidTo;
            authResponseModel.Email = user.Email;
            authResponseModel.Username = user.UserName;
            authResponseModel.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User";
            authResponseModel.RefreshToken = newRefreshToken.Token;
            authResponseModel.RefreshTokenExpiresOn = newRefreshToken.ExpiresOn;
            _logger.LogInformation($"Refresh token successfully generated for user {user.UserName}.");
            return authResponseModel;
        }
    }
}
