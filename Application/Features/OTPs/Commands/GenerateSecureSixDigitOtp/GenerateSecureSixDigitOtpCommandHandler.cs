using MediatR;
using System.Security.Cryptography;

namespace Application.Features.OTPs.Commands.GenerateSecureSixDigitOtp
{
    public class GenerateSecureSixDigitOtpCommandHandler : IRequestHandler<GenerateSecureSixDigitOtpCommand, string>
    {
        public async Task<string> Handle(GenerateSecureSixDigitOtpCommand request, CancellationToken cancellationToken)
        {
            byte[] randomBytes = new byte[4];
            RandomNumberGenerator.Fill(randomBytes);
            int randomValue = Math.Abs(BitConverter.ToInt32(randomBytes, 0));
            int otp = 100000 + (randomValue % 900000);
            return otp.ToString();
        }
    }
}
