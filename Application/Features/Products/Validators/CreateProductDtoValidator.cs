using Application.Features.Products.Dtos;
using Application.Interfaces.IRepositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductDtoValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Stock)
                .GreaterThan(0).WithMessage("Stock must be greater than 0.");

            RuleFor(x => x.SKU)
                .MaximumLength(50).When(x => !string.IsNullOrWhiteSpace(x.SKU))
                .WithMessage("SKU cannot exceed 50 characters.")
                .MustAsync(async (sku, cancellationToken) => !await _productRepository.SkuExistsAsync(sku))
                .WithMessage("SKU must be unique.");

            RuleFor(x => x.Images)
                .NotEmpty().WithMessage("At least one image is required.")
                .Must(images => images.Count <= 6).WithMessage("Maximum 6 images allowed.");

            RuleForEach(x => x.Images).Must(BeAValidFile)
                .WithMessage("Invalid file type. Allowed types: .jpg, .jpeg, .png.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category must be Added.");
            _productRepository = productRepository;
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
