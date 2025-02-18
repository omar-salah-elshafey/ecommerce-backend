namespace Application.Features.Products.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int MaxOrderQuantity { get; set; }
        public string SKU { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }
        public int SalesCount { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductImageDto> Images { get; set; } = new List<ProductImageDto>();
    }

}
