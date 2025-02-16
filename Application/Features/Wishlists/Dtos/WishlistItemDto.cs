namespace Application.Features.Wishlists.Dtos
{
    public record WishlistItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
