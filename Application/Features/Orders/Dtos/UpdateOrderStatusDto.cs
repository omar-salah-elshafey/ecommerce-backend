using Domain.Enums;

namespace Application.Features.Orders.Dtos
{
    public record UpdateOrderStatusDto(Guid OrderId, OrderStatus NewStatus);
}
