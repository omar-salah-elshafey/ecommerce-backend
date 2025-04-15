using Application.ExceptionHandling;
using Application.Features.Authentication.Dtos;
using Application.Features.OTPs.Commands.GenerateAndStoreOtp;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Features.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(UserManager<User> _userManager, ILogger<RegisterUserCommandHandler> _logger,
    IValidator<RegistrationDto> _validator, IEmailService _emailService, IMapper _mapper, IMediator _mediator)
    : IRequestHandler<RegisterUserCommand, AuthResponseModel>
    {
        public async Task<AuthResponseModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var registrationDto = request.registrationDto;
            var validationResult = await _validator.ValidateAsync(registrationDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var role = registrationDto.Role;
            if (await _userManager.FindByEmailAsync(registrationDto.Email.Trim()) is not null)
                throw new DuplicateValueException("البريد الإلكتروني هذا مستخدم بالفعل!");
            
            if (await _userManager.FindByNameAsync(registrationDto.UserName.Trim()) is not null)
                throw new DuplicateValueException("اسم المستخدم هذا مستخدم بالفعل!");

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == registrationDto.PhoneNumber.Trim()))
                throw new DuplicateValueException("رقم الهاتف هذا مستخدم بالفعل!");

            var user = _mapper.Map<User>(registrationDto);
            var result = await _userManager.CreateAsync(user, registrationDto.Password.Trim());
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
                throw new UserCreationException($"حدث خطأ أثناء التسجيل: {errors}");
            }
            await _userManager.AddToRoleAsync(user, registrationDto.Role.ToString());

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var otp = await _mediator.Send(new GenerateAndStoreOtpCommand(user.Email, token));
            var placeholders = new Dictionary<string, string>
            {
                { "UserName", user.UserName },
                { "OTP", otp },
                { "Year", DateTime.Now.Year.ToString() }
            };

            await _emailService.SendEmailAsync(
                user.Email,
                "رمز التحقق من البريد الإلكتروني.",
                "ConfirmEmail",
                placeholders
            );
            _logger.LogInformation("تم إرسال رمز التفعيل إلى بريدك." +
                $"{Environment.NewLine}قم بتفعيل الحساب لتستطيع استخدامه.");
            return new AuthResponseModel
            {
                Email = user.Email,
                Username = user.UserName,
                Role = registrationDto.Role.ToString(),
            };
        }
    }
}
