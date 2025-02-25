using Application.Features.UsersMessages.Dtos;
using FluentValidation;

namespace Application.Features.UsersMessages.Validators
{
    public class SendMessageDtoValidator : AbstractValidator<SendMessageDto>
    {
        public SendMessageDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MinimumLength(10).WithMessage("Full Name must be at least 10 characters.")
                .MaximumLength(100).WithMessage("Full Name must be less than 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not a valid email address.")
                .MaximumLength(100).WithMessage("Email must be less than 100 characters.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MinimumLength(10).WithMessage("Message must be at least 10 characters.")
                .MaximumLength(2000).WithMessage("Message must be less than 2000 characters.");
        }
    }
}
