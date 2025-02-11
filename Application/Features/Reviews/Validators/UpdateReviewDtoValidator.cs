using Application.Features.Reviews.Dtos;
using FluentValidation;

namespace Application.Features.Reviews.Validators
{
    public class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewDtoValidator()
        {
            RuleFor(x => x.Rating).NotEmpty().WithMessage("Rating cannot be empty.")
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comment).MinimumLength(10).WithMessage("Comment must be at least 10 characters long.")
                .When(x => !string.IsNullOrWhiteSpace(x.Comment))
                .MaximumLength(400).WithMessage("Comment cannot exceed 400 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Comment));
        }
    }
}
