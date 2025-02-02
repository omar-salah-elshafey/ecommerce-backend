namespace Application.Interfaces
{
    public interface ICookieService
    {
        void SetRefreshTokenCookie(string refreshToken, DateTime expires);
        void RemoveFromCookies(string key);
        string GetFromCookies(string key);

    }
}
