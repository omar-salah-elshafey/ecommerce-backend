using Application.Features.Products.Dtos;
using Application.Interfaces.IRepositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Application.Features.Products.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Stock)
                .GreaterThan(0).WithMessage("Stock must be greater than 0.");

            RuleForEach(x => x.NewImages).Must(BeAValidFile)
                .WithMessage("Invalid file type. Allowed types: .jpg, .jpeg, .png.");

        }

        private bool BeAValidFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();

            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/jpg", "image/gif" };
            var mimeType = file.ContentType.ToLowerInvariant();
            return allowedExtensions.Contains(extension) && allowedMimeTypes.Contains(mimeType);
        }
    }
}
