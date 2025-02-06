using Application.Features.Authentication.Dtos;
using Domain.Enums;
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
            RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^(?:\+2)?(01(?:0|1|2|5)\d{8})$")
            .WithMessage("Phone number must be in the correct format (e.g. +2010xxxxxxx or 010xxxxxxx)."); ;
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .IsInEnum()
                .WithMessage("Invalid role.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .IsInEnum()
                .WithMessage("Invalid Choice.");

            RuleFor(x => x.MaritalStatus)
                .NotEmpty().WithMessage("MaritalStatus is required.")
                .IsInEnum()
                .WithMessage("Invalid Choice.");

            When(x => x.MaritalStatus == MaritalStatus.Single, () =>
            {
                RuleFor(x => x.HasChildren)
                    .Equal(false)
                    .WithMessage("Single users cannot have children.");

                RuleFor(x => x.ChildrenCount)
                    .Equal(0)
                    .WithMessage("Children count must be 0 if the user is single.");
            });

            When(x => x.MaritalStatus != MaritalStatus.Single, () =>
            {
                When(x => x.HasChildren, () =>
                {
                    RuleFor(x => x.ChildrenCount)
                        .GreaterThanOrEqualTo(1)
                        .WithMessage("Children count must be at least 1 if the user has children.");
                });

                When(x => !x.HasChildren, () =>
                {
                    RuleFor(x => x.ChildrenCount)
                        .Equal(0)
                        .WithMessage("Children count must be 0 if the user has no children.");
                });
            });
        }
    }
}
