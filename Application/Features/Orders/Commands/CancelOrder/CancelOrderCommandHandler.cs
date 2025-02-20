using Application.ExceptionHandling;
using Application.Features.Orders.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandHandler(IOrderRepository _orderRepository, IProductRepository _productRepository, IMapper _mapper, IMediator _mediator) 
        : IRequestHandler<CancelOrderCommand, OrderDto>
    {
        public async Task<OrderDto> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order is null)
                throw new NotFoundException("Order not found!");
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
            if (!userId.Equals(order.UserId))
                throw new ForbiddenAccessException("You cannot perform this action!");
            if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
                throw new InvalidInputsException("Cannot update status of a delivered or cancelled order.");
            order.Status = OrderStatus.Cancelled;
            foreach (var orderItem in order.Items)
            {
                var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                if (product != null)
                {
                    product.Stock += orderItem.Quantity;
                    product.SalesCount -= orderItem.Quantity;
                    await _productRepository.UpdateAsync(product);
                }
            }
            await _orderRepository.UpdateOrderStatusAsync(order);
            return _mapper.Map<OrderDto>(order);
        }
    }
}
