namespace Application.Features.Wishlists.Dtos
{
    public record WishlistItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public int Stock {  get; set; }
        public DateTime AddedAt { get; set; }
    }
}
