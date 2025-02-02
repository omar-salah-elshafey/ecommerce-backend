namespace Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; }
    }
}
