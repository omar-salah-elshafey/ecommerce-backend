using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Application.Features.TokenManagement.GetUserRoleFromToken
{
    public class GetUserRoleFromTokenQueryHandler(ILogger<GetUserRoleFromTokenQueryHandler> _logger, IHttpContextAccessor _httpContextAccessor)
        : IRequestHandler<GetUserRoleFromTokenQuery, string>
    {
        public async Task<string> Handle(GetUserRoleFromTokenQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var role = user.FindFirst(claim => claim.Type == ClaimTypes.Role)?.Value;
            _logger.LogInformation($"User Role From the Token: {role}");
            return role;
        }
    }
}
