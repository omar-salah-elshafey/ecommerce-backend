using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Queries.GetAllOrdersByUser
{
    public record GetAllOrdersByUserQuery(string UserName) : IRequest<List<OrderDto>>;
}
