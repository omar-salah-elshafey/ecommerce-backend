using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.OTPs.Queries.GetTokenFromOtp
{
    public class GetTokenFromOtpQueryHandler(IOtpRepository _otpRepository) : IRequestHandler<GetTokenFromOtpQuery, (string Token, bool IsExpired)>
    {
        public async Task<(string Token, bool IsExpired)> Handle(GetTokenFromOtpQuery request, CancellationToken cancellationToken)
        {
            var otpEntity = await _otpRepository.GetByEmailAndOtpAsync(request.Email, request.Otp);
            if (otpEntity == null)
            {
                return (null, false);
            }

            bool isExpired = otpEntity.ExpirationDateTime <= DateTime.UtcNow;
            string token = isExpired ? null : otpEntity.Token;

            await _otpRepository.DeleteAsync(otpEntity);

            return (token, isExpired);
        }
    }
}
