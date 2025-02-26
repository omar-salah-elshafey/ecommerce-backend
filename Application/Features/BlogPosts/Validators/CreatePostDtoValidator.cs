using Application.Features.BlogPosts.Dtos;
using FluentValidation;

namespace Application.Features.BlogPosts.Validators
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(x => x.Title.Trim())
                .NotEmpty().WithMessage("Title cannot be empty")
                .MinimumLength(10).WithMessage("Title cannot be less than 10 chars")
                .MaximumLength(200).WithMessage("Title cannot be more than 200 chars");
            RuleFor(x => x.Content.Trim())
                .NotEmpty().WithMessage("Content cannot be empty")
                .MinimumLength(10).WithMessage("Content cannot be less than 10 chars")
                .MaximumLength(2000).WithMessage("Content cannot be more than 2000 chars");
            RuleFor(x => x.ReadTime)
                .NotEmpty().WithMessage("Reading time cannot be empty")
                .GreaterThan(0).WithMessage("Reading time cannot be less than 1 minute");
        }
    }
}
