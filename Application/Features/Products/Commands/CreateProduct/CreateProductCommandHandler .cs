using Application.ExceptionHandling;
using Application.Features.Products.Dtos;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(IProductRepository _productRepository, IMapper _mapper, IValidator<CreateProductDto> _validator,
        IFileService _fileService, ICategoryRepository _categoryRepository, IHttpContextAccessor _httpContextAccessor, ILogger<CreateProductCommandHandler> _logger)
        : IRequestHandler<CreateProductCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateProductDto;
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name.Trim(),
                Description = dto.Description.Trim(),
                Price = dto.Price,
                Stock = dto.Stock,
                SKU = dto.SKU.Trim(),
                IsFeatured = dto.IsFeatured,
                SalesCount = 0,
                IsDeleted = false,
                Images = new List<ProductImage>(),
            };

            var uploadedFiles = new List<string>();
            try
            {
                if (dto.Images != null && dto.Images.Any())
                {
                    foreach (var file in dto.Images)
                    {
                        string imageUrl = await _fileService.UploadFileAsync(file);
                        uploadedFiles.Add(imageUrl);
                        product.Images.Add(new ProductImage
                        {
                            Id = Guid.NewGuid(),
                            ImageUrl = imageUrl,
                            IsMain = product.Images.Count == 0,
                            ProductId = product.Id,
                        });
                    }
                }
                var categoryId = dto.CategoryId;
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                _logger.LogInformation($"The provided CatID: {categoryId}");
                if (category is null)
                    throw new NotFoundException($"Category not found! {categoryId}");
                product.CategoryId = categoryId;
                await _productRepository.AddAsync(product);
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
