using Application.ExceptionHandling;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.PasswordManagement.Commands.VerifyResetPasswordRequest
{
    public class VerifyResetPasswordRequestCommandHandler(ILogger<VerifyResetPasswordRequestCommandHandler> logger,
        UserManager<User> _userManager) : IRequestHandler<VerifyResetPasswordRequestCommand>
    {
        public async Task Handle(VerifyResetPasswordRequestCommand request,
            CancellationToken cancellationToken)
        {
            var confirmEmailDto = request.ConfirmEmailDto;
            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email.Trim());
            if (user == null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");

            var result = await _userManager.VerifyUserTokenAsync(user,
                _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", confirmEmailDto.Token.Trim());
            if (!result)
                throw new InvalidTokenException("الرمز غير صالح!");

            logger.LogInformation("Your Password reset request is verified.");
        }
    }
}
