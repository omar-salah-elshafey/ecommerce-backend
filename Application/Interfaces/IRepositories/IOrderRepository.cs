using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task<List<Order>> GetOrdersByUserNameAsync(string userName);
        Task<List<Order>> GetInProgressOrdersByUserNameAsync(string userName);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetAllInProgressOrdersAsync();
    }
}
