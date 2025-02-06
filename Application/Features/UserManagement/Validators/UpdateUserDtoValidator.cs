using Application.Features.UserManagement.Dtos;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.UserManagement.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty()
                .MinimumLength(3).WithMessage("FirstName Can't be less that 3 Characters.")
                .MaximumLength(50).WithMessage("FirstName Can't be more that 50 Characters.");

            RuleFor(x => x.LastName).NotEmpty()
                .MinimumLength(3).WithMessage("LastName Can't be less that 3 Characters.")
                .MaximumLength(50).WithMessage("LastName Can't be more that 50 Characters.");

            RuleFor(x => x.MaritalStatus)
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
