using Application.Features.Authentication.Dtos;
using FluentValidation;

namespace Application.Features.Authentication.Validators
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("Invalid role.");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage("Invalid Choice.");

            RuleFor(x => x.MaritalStatus)
                .IsInEnum()
                .WithMessage("Invalid Choice.");

            When(x => !x.HasChildren, () =>
            {
                RuleFor(x => x.ChildrenCount)
                    .Equal(0)
                    .WithMessage("Children count must be 0 if the user has no children.");
            });

            When(x => x.HasChildren, () =>
            {
                RuleFor(x => x.ChildrenCount)
                    .GreaterThanOrEqualTo(1)
                    .WithMessage("Children count must be at least 1 if the user has children.");
            });
        }
    }
}
