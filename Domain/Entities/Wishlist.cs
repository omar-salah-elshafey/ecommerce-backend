namespace Domain.Entities
{
    public class Wishlist
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
