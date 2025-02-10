using Application.Features.Categories.Dtos;
using FluentValidation;

namespace Application.Features.Categories.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).WithMessage("Catigory can't be less that 3 characters")
                .MaximumLength(100).WithMessage("Catigory can't be more that 100 characters");
        }
    }
}
