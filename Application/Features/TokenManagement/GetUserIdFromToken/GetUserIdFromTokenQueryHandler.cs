using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Features.TokenManagement.GetUserIdFromToken
{
    public class GetUserIdFromTokenQueryHandler(IHttpContextAccessor _httpContextAccessor, ILogger<GetUserIdFromTokenQueryHandler> _logger) : IRequestHandler<GetUserIdFromTokenQuery, string>
    {
        public async Task<string?> Handle(GetUserIdFromTokenQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                _logger.LogWarning("User is not authenticated");
                return null;
            }
            var userId = user.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            _logger.LogWarning($"Current User: {userId}");
            return userId;
        }
    }
}
