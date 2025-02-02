using Application.ExceptionHandling;
using Application.Features.Authentication.Dtos;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Features.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(UserManager<User> _userManager, ILogger<RegisterUserCommandHandler> _logger,
    IValidator<RegistrationDto> _validator, IOptions<DataProtectionTokenProviderOptions> _tokenProviderOptions, IEmailService _emailService)
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
                throw new DuplicateEmailException("This Email is already used!");
            
            if (await _userManager.FindByNameAsync(registrationDto.UserName) is not null)
                throw new DuplicateUsernameException("This Username is already used!");

            // Create the new user
            var user = new User
            {
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                UserName = registrationDto.UserName,
                Email = registrationDto.Email,
                PhoneNumber = registrationDto.PhoneNumber,
                Gender = registrationDto.Gender,
                MaritalStatus = registrationDto.MaritalStatus,
                HasChildren = registrationDto.HasChildren,
                ChildrenCount = registrationDto.HasChildren ? registrationDto.ChildrenCount : 0

            };
            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
                throw new UserCreationException($"Failed to create user: {errors}");
            }
            // Assign the user to the specified role
            await _userManager.AddToRoleAsync(user, registrationDto.Role.ToString());

            //generating the token to verify the user's email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Dynamically get the expiration time from the options
            var expirationTime = _tokenProviderOptions.Value.TokenLifespan.TotalMinutes;

            //await _emailService.SendEmailAsync(user.Email, "Email Verification Code.",
            //    $"Hello {user.UserName}, Use this new token to verify your Email: {token}{Environment.NewLine}This code is Valid only for {expirationTime} Minutes.");
            _logger.LogInformation("A verification code has been sent to your Email." +
                $"{Environment.NewLine}Verify Your Email to be able to login :) ");
            return new AuthResponseModel
            {
                Email = user.Email,
                Username = user.UserName,
                Role = registrationDto.Role.ToString(),
            };
        }
    }
}
