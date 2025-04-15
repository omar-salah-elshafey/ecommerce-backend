using Application.Features.Orders.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs
{
    public class OrderHub(IOrderNotificationService _notificationService) : Hub
    {
        public async Task NotifyNewOrder(OrderDto order)
        {
            await Clients.Group("Admins").SendAsync("ReceiveNewOrder", order);
        }

        public async Task JoinAdminGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            var notifications = await _notificationService.GetUnprocessedNotificationsAsync();
            await Clients.Caller.SendAsync("ReceiveUnprocessedNotifications", notifications);
        }

        public async Task NotifyOrderStatusUpdate(Guid orderId, string newStatus)
        {
            await Clients.Group($"Order_{orderId}").SendAsync("ReceiveStatusUpdate", orderId, newStatus);
        }

        public async Task JoinOrderGroup(Guid orderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Order_{orderId}");
        }
    }
}
