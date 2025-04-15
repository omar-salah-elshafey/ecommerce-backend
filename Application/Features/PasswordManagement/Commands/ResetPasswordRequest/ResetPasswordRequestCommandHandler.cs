using Application.ExceptionHandling;
using Application.Features.OTPs.Commands.GenerateAndStoreOtp;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.PasswordManagement.Commands.ResetPasswordRequest
{
    public class ResetPasswordRequestCommandHandler(UserManager<User> _userManager, IEmailService _emailService,
        ILogger<ResetPasswordRequestCommandHandler> logger, IMediator _mediator)
        : IRequestHandler<ResetPasswordRequestCommand>
    {
        public async Task Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
        {
            var email = request.email.Trim();
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var otp = await _mediator.Send(new GenerateAndStoreOtpCommand(user.Email, token));
            var placeholders = new Dictionary<string, string>
            {
                { "UserName", user.UserName },
                { "OTP", otp },
                { "Year", DateTime.Now.Year.ToString() }
            };
            await _emailService.SendEmailAsync(
                email,
                "إعادة تعيين كلمة المرور.",
                "PasswordReset",
                placeholders
                );
            logger.LogInformation($"A Password Reset OTP has been sent to {email}");
        }
    }
}
