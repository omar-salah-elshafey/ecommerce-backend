using Application.ExceptionHandling;
using Application.Features.OTPs.Queries.GetTokenFromOtp;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.PasswordManagement.Commands.VerifyResetPasswordRequest
{
    public class VerifyResetPasswordRequestCommandHandler(ILogger<VerifyResetPasswordRequestCommandHandler> _logger,
        UserManager<User> _userManager, IMediator _mediator) : IRequestHandler<VerifyResetPasswordRequestCommand>
    {
        public async Task Handle(VerifyResetPasswordRequestCommand request,
            CancellationToken cancellationToken)
        {
            var confirmEmailDto = request.ConfirmEmailDto;
            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email.Trim());
            if (user == null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");

            var (realToken, isExpired) = await _mediator.Send(new GetTokenFromOtpQuery(confirmEmailDto.Email, confirmEmailDto.Token));
            if (realToken == null)
            {
                if (isExpired)
                {
                    _logger.LogWarning("OTP has expired for user {Email}", user.Email);
                    throw new InvalidTokenException("The OTP has expired. Please request a new one.");
                }
                _logger.LogWarning("Invalid OTP for user {Email}", user.Email);
                throw new InvalidTokenException("OTP is not valid.");
            }

            var result = await _userManager.VerifyUserTokenAsync(user,
                _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", realToken);
            if (!result)
                throw new InvalidTokenException("الرمز غير صالح!");

            _logger.LogInformation("Your Password reset request is verified.");
        }
    }
}
