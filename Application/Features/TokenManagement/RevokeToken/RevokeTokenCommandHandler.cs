using Application.ExceptionHandling;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.TokenManagement.RevokeToken
{
    public class RevokeTokenCommandHandler(UserManager<User> _userManager, ILogger<RevokeTokenCommandHandler> _logger)
        : IRequestHandler<RevokeTokenCommand>
    {
        public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var token = request.token;
            token = Uri.UnescapeDataString(token);
            
            var user = await _userManager.Users.Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            
            if (user == null)
            {
                _logger.LogWarning("Token revocation failed: user not found.");
                throw new InvalidTokenException("Token revocation failed: user not found.");
            }
                
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                _logger.LogWarning($"Token revocation failed: token already inactive for user {user.UserName}");
                throw new InvalidTokenException($"Token revocation failed: token already inactive for user {user.UserName}");
            }
            
            refreshToken.RevokedOn = DateTime.UtcNow;
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogError($"Token revocation failed for user {user.UserName} due to update failure.");
                throw new Exception("Failed to update user after token removal.");
            }
            _logger.LogInformation($"Token revoked successfully for user {user.UserName} at {DateTime.UtcNow.ToLocalTime()}");
        }
    }
}
