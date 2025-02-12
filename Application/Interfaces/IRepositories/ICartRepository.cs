using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetByUserIdAsync(string userId);
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task AddItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
    }
}
