using Application.ExceptionHandling;
using Application.Features.Orders.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandHandler(IOrderRepository _orderRepository, IProductRepository _productRepository, IMapper _mapper, 
        IMediator _mediator, IEmailService _emailService) 
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
            var placeHolder = new Dictionary<string, string>
            {
                {"UserName", order.User.UserName},
                {"OrderId", order.Id.ToString()},
                {"OrderDate", order.OrderDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")},
                {"TotalAmount", order.TotalAmount.ToString("F2")},
                {"NewStatus", OrderStatus.Cancelled.ToString()},
                {"CancelledDate", DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")},
                {"OrderUrl", $"http://localhost:5000/orders/{order.Id}"}
            };
            await _emailService.SendEmailAsync(
                order.User.Email,
                "إلغاء الطلب",
                "OrderCancelled",
                placeHolder
            );
            return _mapper.Map<OrderDto>(order);
        }
    }
}
