using Application.Features.UserManagement.Dtos;
using FluentValidation;

namespace Application.Features.UserManagement.Validators
{
    public class ChangeUserRoleDtoValidator: AbstractValidator<ChangeUserRoleDto>
    {
        public ChangeUserRoleDtoValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty()
                .MinimumLength(3).WithMessage("UserName Can't be less that 3 Characters.")
                .MaximumLength(50).WithMessage("UserName Can't be more that 50 Characters.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .IsInEnum()
                .WithMessage("Invalid role.");
        }
    }
}
