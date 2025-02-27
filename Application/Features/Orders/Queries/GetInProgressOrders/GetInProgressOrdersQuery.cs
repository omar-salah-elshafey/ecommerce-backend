using Application.Features.Orders.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Orders.Queries.GetInProgressOrders
{
    public record GetInProgressOrdersQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<OrderDto>>;
}
