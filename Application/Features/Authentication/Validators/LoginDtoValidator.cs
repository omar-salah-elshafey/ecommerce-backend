using Application.Features.Authentication.Dtos;
using FluentValidation;

namespace Application.Features.Authentication.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.EmailOrUserName)
                .NotEmpty()
                .WithMessage("Email or username is required.")
                .MaximumLength(50)
                .WithMessage("Email or username must not exceed 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MaximumLength(50)
                .WithMessage("Password must not exceed 50 characters.");
        }
    }
}
