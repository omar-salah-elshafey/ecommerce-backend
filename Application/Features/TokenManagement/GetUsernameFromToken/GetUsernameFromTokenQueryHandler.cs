using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.TokenManagement.GetUsernameFromToken
{
    public class GetUsernameFromTokenQueryHandler(ILogger<GetUsernameFromTokenQueryHandler> _logger, IHttpContextAccessor _httpContextAccessor)
        : IRequestHandler<GetUsernameFromTokenQuery, string>
    {
        public async Task<string> Handle(GetUsernameFromTokenQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var username = user.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
            _logger.LogInformation($"UserName From the Token: {username}");
            return username;
        }
    }
}
