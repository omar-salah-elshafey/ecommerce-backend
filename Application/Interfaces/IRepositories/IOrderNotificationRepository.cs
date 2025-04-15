using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IOrderNotificationRepository
    {
        Task AddAsync(OrderNotification notification);
        Task<OrderNotification?> GetByOrderIdAsync(Guid orderId);
        Task UpdateAsync(OrderNotification notification);
        Task<IEnumerable<OrderNotification>> GetUnprocessedAsync();
    }
}
