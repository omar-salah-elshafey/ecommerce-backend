using MediatR;

namespace Application.Features.OTPs.Queries.GetTokenFromOtp
{
    public record GetTokenFromOtpQuery(string Email, string Otp): IRequest<(string Token, bool IsExpired)>;
}
