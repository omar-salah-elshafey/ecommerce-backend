using Application.ExceptionHandling;
using Application.Features.Authentication.Dtos;
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
    IValidator<RegistrationDto> _validator, IOptions<DataProtectionTokenProviderOptions> _tokenProviderOptions,
    IEmailService _emailService, IMapper _mapper)
    : IRequestHandler<RegisterUserCommand, AuthResponseModel>
    {
        public async Task<AuthResponseModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var registrationDto = request.registrationDto;
            var validationResult = await _validator.ValidateAsync(registrationDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var role = registrationDto.Role;
            //check if user exists
            if (await _userManager.FindByEmailAsync(registrationDto.Email) is not null)
                throw new DuplicateValueException("البريد الإلكتروني هذا مستخدم بالفعل!");
            
            if (await _userManager.FindByNameAsync(registrationDto.UserName) is not null)
                throw new DuplicateValueException("اسم المستخدم هذا مستخدم بالفعل!");

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == registrationDto.PhoneNumber))
                throw new DuplicateValueException("رقم الهاتف هذا مستخدم بالفعل!");

            // Create the new user
            var user = _mapper.Map<User>(registrationDto);
            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
                throw new UserCreationException($"حدث خطأ أثناء التسجيل: {errors}");
            }
            // Assign the user to the specified role
            await _userManager.AddToRoleAsync(user, registrationDto.Role.ToString());

            //generating the token to verify the user's email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Dynamically get the expiration time from the options
            var expirationTime = _tokenProviderOptions.Value.TokenLifespan.TotalMinutes;

            //await _emailService.SendEmailAsync(user.Email, "رمز التحقق من البريد الإلكتروني.",
            //    $"أهلا يا  {user.UserName}, استخدم هذا الرمز لتفعيل حسابك: {token}{Environment.NewLine}هذا الرمز صالح لمدة: {expirationTime} دقائق فقط.");
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
