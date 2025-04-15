namespace Application.Features.Orders.Dtos
{
    public record OrderNotificationDto(Guid Id, Guid OrderId, DateTime CreatedAt, OrderDto Order);
}
