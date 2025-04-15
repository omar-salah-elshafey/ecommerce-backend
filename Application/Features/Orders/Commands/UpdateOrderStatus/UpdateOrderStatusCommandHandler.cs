using Application.ExceptionHandling;
using Application.Features.Orders.Dtos;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusHandler(IOrderRepository _orderRepository, IMapper _mapper, IEmailService _emailService,
        IValidator<UpdateOrderStatusDto> _validator)
        : IRequestHandler<UpdateOrderStatusCommand, OrderDto>
    {
        public async Task<OrderDto> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateOrderStatusDto;
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var order = await _orderRepository.GetByIdAsync(dto.OrderId);
            if (order is null)
                throw new NotFoundException("Order not found!");
            if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
                throw new InvalidInputsException("Cannot update status of a delivered or cancelled order.");
            order.Status = dto.NewStatus;
            await _orderRepository.UpdateOrderStatusAsync(order);
            var placeHolder = new Dictionary<string, string>
            {
                {"UserName", order.User.UserName},
                {"OrderId", order.Id.ToString()},
                {"OrderDate", order.OrderDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")},
                {"TotalAmount", order.TotalAmount.ToString("F2")},
                {"NewStatus", order.Status.ToString()},
                {"UpdatedDate", DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")},
                {"OrderUrl", $"http://localhost:5000/orders/{order.Id}"}
            };
            await _emailService.SendEmailAsync(
                order.User.Email,
                "تحديث حالة الطلب",
                "OrderStatusUpdated",
                placeHolder
            );

            return _mapper.Map<OrderDto>(order);
        }
    }
}
