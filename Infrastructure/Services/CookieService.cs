using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class CookieService: ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetRefreshTokenCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Expires = expires.ToLocalTime(),
                Secure = false,
                SameSite = SameSiteMode.Strict
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        public void RemoveFromCookies(string key)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(-1).ToLocalTime(),
                Secure = true, // Set to true in production
                SameSite = SameSiteMode.Strict
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, "", cookieOptions);
        }

        public string GetFromCookies(string key)
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies[key];
        }
    }
}
