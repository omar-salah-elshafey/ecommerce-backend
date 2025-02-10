using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}
