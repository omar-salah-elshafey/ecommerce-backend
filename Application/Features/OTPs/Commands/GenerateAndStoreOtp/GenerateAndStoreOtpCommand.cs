using MediatR;

namespace Application.Features.OTPs.Commands.GenerateAndStoreOtp
{
    public record GenerateAndStoreOtpCommand(string Email, string Token): IRequest<string>;
}
