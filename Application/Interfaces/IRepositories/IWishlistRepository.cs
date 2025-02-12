using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IWishlistRepository
    {
        Task<Wishlist?> GetByUserIdAsync(string userId);
        Task AddAsync(Wishlist wishlist);
        Task UpdateAsync(Wishlist wishlist);
        Task AddItemAsync(WishlistItem wishlistItem);
    }

}
