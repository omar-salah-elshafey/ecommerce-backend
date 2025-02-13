using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(CreateOrderDto CreateOrderDto) : IRequest<OrderDto>;
}
