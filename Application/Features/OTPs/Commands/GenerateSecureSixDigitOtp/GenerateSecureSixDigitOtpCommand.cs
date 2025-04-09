using MediatR;

namespace Application.Features.OTPs.Commands.GenerateSecureSixDigitOtp
{
    public record GenerateSecureSixDigitOtpCommand : IRequest<string>;
}
