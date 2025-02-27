using Application.Features.Orders.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public record GetAllOrdersQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<OrderDto>>;
}
