using Application.Features.Orders.Dtos;
using Application.Interfaces.IRepositories;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services
{
    public class OrderNotificationService(IHubContext<OrderHub> _hubContext, IOrderNotificationRepository _notificationRepository,
        IMapper _mapper) : IOrderNotificationService
    {

        public async Task NotifyNewOrderAsync(OrderDto order)
        {
            var notification = new OrderNotification
            {
                OrderId = order.Id,
                CreatedAt = DateTime.UtcNow,
                IsProcessed = false
            };
            await _notificationRepository.AddAsync(notification);

            await _hubContext.Clients.Group("Admins").SendAsync("ReceiveNewOrder", order);
        }

        public async Task MarkNotificationAsProcessedAsync(Guid orderId)
        {
            var notification = await _notificationRepository.GetByOrderIdAsync(orderId);
            if (notification != null && !notification.IsProcessed)
            {
                notification.IsProcessed = true;
                await _notificationRepository.UpdateAsync(notification);
            }
        }

        public async Task<IEnumerable<OrderNotificationDto>> GetUnprocessedNotificationsAsync()
        {
            var notifications = await _notificationRepository.GetUnprocessedAsync();
            return _mapper.Map<IEnumerable<OrderNotificationDto>>(notifications);
        }
    }
}
