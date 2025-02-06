namespace Domain.Entities
{
    public class WishlistItem
    {
        public Guid Id { get; set; }
        public Guid WishlistId { get; set; }
        public Guid ProductId { get; set; }
        public decimal SnapShotPrice { get; set; }
        public DateTime AddedAt { get; set; }
        public Product Product { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
