using Application.ExceptionHandling;
using Application.Features.PasswordManagement.Dtos;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.PasswordManagement.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler(ILogger<ChangePasswordCommandHandler> logger, UserManager<User> _userManager, 
        IValidator<ChangePasswordDto> _validator) : IRequestHandler<ChangePasswordCommand>
    {
        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var changePasswordDto = request.changePasswordDto;
            var validationResult = await _validator.ValidateAsync(changePasswordDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var user = await _userManager.FindByEmailAsync(changePasswordDto.Email.Trim());
            if (user == null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");

            if (changePasswordDto.CurrentPassword.Trim().Equals(changePasswordDto.NewPassword.Trim()))
                throw new InvalidInputsException("لا يمكن أن تكون كلمات المرور القديمة والجديدة متطابقة!");


            var result = await _userManager.ChangePasswordAsync(user,
                changePasswordDto.CurrentPassword.Trim(), changePasswordDto.NewPassword.Trim());
            if (!result.Succeeded)
                throw new InvalidInputsException("كلمة المرور غير صحيحة!");

            logger.LogInformation("Your password has been updated successfully.");
        }
    }
}
