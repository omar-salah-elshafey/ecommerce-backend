using Application.Features.TokenManagement.RevokeToken;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Application.Features.Authentication.Commands.Logout
{
    public class LogoutCommandHandler(UserManager<User> _userManager, IMediator _mediator, ICookieService _cookieService,
        ILogger<LogoutCommandHandler> _logger, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<LogoutCommand>
    {
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = request.refreshToken;
            var userClaims = _httpContextAccessor.HttpContext?.User;
            var userId = userClaims!.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _mediator.Send(new RevokeTokenCommand(refreshToken));
            _logger.LogInformation("Refreshtoken revoked.");
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                user.Online = false;
                await _userManager.UpdateAsync(user);
                _logger.LogInformation("User logged out successfully.");
                _cookieService.RemoveFromCookies("refreshToken");
            }catch (Exception ex)
            {
                throw;
            }
        }
    }
}
