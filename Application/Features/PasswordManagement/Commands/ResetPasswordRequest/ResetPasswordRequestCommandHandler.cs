using Application.ExceptionHandling;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Features.PasswordManagement.Commands.ResetPasswordRequest
{
    public class ResetPasswordRequestCommandHandler(UserManager<User> _userManager, IEmailService _emailService,
        IOptions<DataProtectionTokenProviderOptions> _tokenProviderOptions
        , ILogger<ResetPasswordRequestCommandHandler> logger)
        : IRequestHandler<ResetPasswordRequestCommand>
    {
        public async Task Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
        {
            var email = request.email;
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");

            //generating the token to verify the user's email
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Dynamically get the expiration time from the options
            var expirationTime = _tokenProviderOptions.Value.TokenLifespan.TotalMinutes;

            await _emailService.SendEmailAsync(email, "إعادة تعيين كلمة المرور.",
                $"أهلا {user.UserName}, استخدم هذا الرمز لتغيير كلمة المرور الخاصة بك: {token}\n الرمز صالح لمدة {expirationTime} دقائق فقط.");
            logger.LogInformation("A Password Reset Code has been sent to your Email!");
        }
    }
}
