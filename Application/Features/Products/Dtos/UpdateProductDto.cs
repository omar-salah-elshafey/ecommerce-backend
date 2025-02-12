using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Dtos
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int MaxOrderQuantity { get; set; }
        public bool IsFeatured { get; set; }
        public List<Guid> ImagesToDelete { get; set; } = new();
        public List<IFormFile> NewImages { get; set; } = new();
        public Guid? CategoryId { get; set; } = new();
    }
}
