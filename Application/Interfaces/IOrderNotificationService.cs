using Application.Features.Orders.Dtos;

namespace Application.Interfaces
{
    public interface IOrderNotificationService
    {
        Task NotifyNewOrderAsync(OrderDto order);
        Task MarkNotificationAsProcessedAsync(Guid orderId);
        Task<IEnumerable<OrderNotificationDto>> GetUnprocessedNotificationsAsync();
    }
}
