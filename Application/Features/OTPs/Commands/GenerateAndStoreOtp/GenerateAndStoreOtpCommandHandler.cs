using Application.Features.OTPs.Commands.GenerateSecureSixDigitOtp;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.OTPs.Commands.GenerateAndStoreOtp
{
    public class GenerateAndStoreOtpCommandHandler(IOtpRepository _otpRepository, IMediator _mediator) : IRequestHandler<GenerateAndStoreOtpCommand, string>
    {
        private readonly TimeSpan _otpLifespan = TimeSpan.FromMinutes(10);
        public async Task<string> Handle(GenerateAndStoreOtpCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email;
            var token = request.Token;
            await _otpRepository.DeleteOtpsByEmailAsync(email);

            var otpCode = await _mediator.Send(new GenerateSecureSixDigitOtpCommand());
            var otp = new Otp
            {
                Email = email,
                OtpCode = otpCode,
                Token = token,
                ExpirationDateTime = DateTime.UtcNow + _otpLifespan
            };

            await _otpRepository.AddAsync(otp);
            return otpCode;
        }
    }
}
