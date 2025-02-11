using Application.ExceptionHandling;
using Application.Features.Products.Dtos;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductRepository _productRepository, IMapper _mapper, IImageService _imageService, 
        ICategoryRepository _categoryRepository, IFileService _fileService, IValidator<UpdateProductDto> _validator, 
        ILogger<UpdateProductCommandHandler> _logger) 
        : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ProductDto;
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var productId = request.ProductId;
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new NotFoundException("Product not found");

            product.Name = dto.Name.Trim();
            product.Description = dto.Description.Trim();
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.IsFeatured = dto.IsFeatured;

            if (dto.CategoryId.HasValue)
            {
                var categoryId = dto.CategoryId.Value;
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category is null)
                    throw new NotFoundException($"Category not found! {categoryId}");
                product.CategoryId = categoryId;
            }

            if (dto.ImagesToDelete.Any())
            {
                product.Images = product.Images
                    .Where(img => !dto.ImagesToDelete.Contains(img.Id))
                    .ToList();

                await _imageService.DeleteImagesAsync(dto.ImagesToDelete);
            }

            if (product.Images.Count == 0 && (dto.NewImages == null || !dto.NewImages.Any()))
                throw new InvalidInputsException("At least one image must be kept for the product.");

            var uploadedFiles = new List<string>();
            try
            {
                if (dto.NewImages != null && dto.NewImages.Any())
                {
                    if (product.Images.Count + dto.NewImages.Count > 6)
                        throw new ValidationException("Maximum 6 images allowed");
                    foreach (var file in dto.NewImages)
                    {
                        string imageUrl = await _fileService.UploadFileAsync(file);
                        uploadedFiles.Add(imageUrl);
                        product.Images.Add(new ProductImage
                        {
                            Id = Guid.NewGuid(),
                            ImageUrl = imageUrl,
                            ProductId = product.Id,
                        });
                    }
                }
                if (!product.Images.Any(img => img.IsMain))
                {
                    var firstImage = product.Images.FirstOrDefault();
                    if (firstImage != null)
                        firstImage.IsMain = true;
                }
                await _productRepository.UpdateAsync(product);

            }
            catch
            {
                foreach (var fileUrl in uploadedFiles)
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileUrl.TrimStart('/'));
                    if (File.Exists(fullPath)) File.Delete(fullPath);
                }
                throw;
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
