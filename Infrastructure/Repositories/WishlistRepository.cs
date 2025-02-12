using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WishlistRepository(ApplicationDbContext _context) : IWishlistRepository
    {
        public async Task<Wishlist?> GetByUserIdAsync(string userId)
        {
            return await _context.Wishlists
                .Include(w => w.WishlistItems)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task AddAsync(Wishlist wishlist)
        {
            await _context.Wishlists.AddAsync(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Wishlist wishlist)
        {
            _context.Wishlists.Update(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task AddItemAsync(WishlistItem wishlistItem)
        {
            await _context.WishlistItems.AddAsync(wishlistItem);
            await _context.SaveChangesAsync();
        }
    }
}
