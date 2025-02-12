using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Dtos
{
    public record CreateProductDto
        (string Name, string Description, decimal Price, int Stock, int MaxOrderQuantity, string SKU, bool IsFeatured, List<IFormFile> Images, Guid CategoryId);
}
