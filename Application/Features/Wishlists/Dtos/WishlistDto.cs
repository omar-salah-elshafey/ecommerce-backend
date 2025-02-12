namespace Application.Features.Wishlists.Dtos
{
    public class WishlistDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<WishlistItemDto> Items { get; set; } = new();
    }
}
