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

            await _emailService.SendEmailAsync(email, "إعادة تعيين كلمة المرور.",
                $"أهلا {user.UserName}, استخدم هذا الرمز لتغيير كلمة المرور الخاصة بك: {otp}\n الرمز صالح لمدة 10 دقائق فقط.");
            logger.LogInformation($"A Password Reset OTP has been sent to {email}");
        }
    }
}
