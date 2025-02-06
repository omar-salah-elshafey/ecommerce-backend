using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string SKU { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProductImage> Images { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public bool IsFeatured { get; set; }
        public int SalesCount { get; set; }
    }
}
