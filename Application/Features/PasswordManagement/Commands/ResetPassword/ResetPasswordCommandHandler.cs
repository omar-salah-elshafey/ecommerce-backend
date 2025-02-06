using Application.ExceptionHandling;
using Application.Features.PasswordManagement.Dtos;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.PasswordManagement.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler(UserManager<User> _userManager, ILogger<ResetPasswordCommandHandler> logger,
        IValidator<ResetPasswordDto> _validator) : IRequestHandler<ResetPasswordCommand>
    {
        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var resetPasswordDto = request.resetPasswordDto;
            var validationResult = await _validator.ValidateAsync(resetPasswordDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
                throw new InvalidTokenException("الرمز غير صالح!");

            logger.LogInformation("Your password has been reset successfully.");
        }
    }
}
