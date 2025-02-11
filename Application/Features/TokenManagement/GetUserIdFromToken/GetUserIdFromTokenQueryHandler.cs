using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.TokenManagement.GetUserIdFromToken
{
    public class GetUserIdFromTokenQueryHandler(IHttpContextAccessor _httpContextAccessor) : IRequestHandler<GetUserIdFromTokenQuery, string>
    {
        public async Task<string> Handle(GetUserIdFromTokenQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            return userId;
        }
    }
}
