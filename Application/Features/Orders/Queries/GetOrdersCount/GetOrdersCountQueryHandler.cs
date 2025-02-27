using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrdersCount
{
    public class GetOrdersCountQueryHandler(IOrderRepository _orderRepository) : IRequestHandler<GetOrdersCountQuery, int>
    {
        public async Task<int> Handle(GetOrdersCountQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetOrdersCountAsync();
        }
    }
}
