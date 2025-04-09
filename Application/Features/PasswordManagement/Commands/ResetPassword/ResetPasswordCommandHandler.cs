using Application.ExceptionHandling;
using Application.Features.PasswordManagement.Dtos;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.PasswordManagement.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler(UserManager<User> _userManager, ILogger<ResetPasswordCommandHandler> _logger,
        IValidator<ResetPasswordDto> _validator) : IRequestHandler<ResetPasswordCommand>
    {
        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var resetPasswordDto = request.resetPasswordDto;
            var validationResult = await _validator.ValidateAsync(resetPasswordDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email.Trim());
            if (user == null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");

            var hashedPassword = _userManager.PasswordHasher.HashPassword(user, resetPasswordDto.NewPassword);
            user.PasswordHash = hashedPassword;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                _logger.LogWarning("Password reset failed for user {Email}", user.Email);
                throw new InvalidTokenException("فضل إعادة تعيين كلمة السر.");
            }

            _logger.LogInformation("Your password has been reset successfully.");
        }
    }
}
