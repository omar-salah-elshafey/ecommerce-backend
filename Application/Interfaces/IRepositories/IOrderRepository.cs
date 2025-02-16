using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.IRepositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateOrderStatusAsync(Order order);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task<List<Order>> GetOrdersByUserNameAsync(string userName);
        Task<List<Order>> GetInProgressOrdersByUserNameAsync(string userName);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetAllInProgressOrdersAsync();
    }
}
