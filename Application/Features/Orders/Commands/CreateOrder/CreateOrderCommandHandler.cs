using Application.ExceptionHandling;
using Application.Features.Orders.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Features.TokenManagement.GetUsernameFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IOrderRepository _orderRepository, ICartRepository _cartRepository, IProductRepository _productRepository,
    IMapper _mapper, IMediator _mediator, ICityRepository _cityRepository, IGovernorateRepository _governorateRepository, 
    IValidator<CreateOrderDto> _validator)
    : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDto = request.CreateOrderDto;

            var validationResult = await _validator.ValidateAsync(orderDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var governorate = await _governorateRepository.GetByIdAsync(orderDto.GovernorateId);
            if (governorate == null)
                throw new NotFoundException("Invalid Governorate.");

            var city = await _cityRepository.GetByIdAsync(orderDto.CityId);
            if (city == null)
                throw new NotFoundException("Invalid City.");

            if (city.GovernorateId != orderDto.GovernorateId)
                throw new InvalidInputsException("The selected city does not belong to the chosen governorate.");

            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
            var userName = await _mediator.Send(new GetUsernameFromTokenQuery());

            var cart = await _cartRepository.GetByUserIdAsync(userId);
            if (cart == null || cart.Items == null || !cart.Items.Any())
                throw new InvalidInputsException("Your cart is empty.");
            decimal totalAmount = 0;

            foreach (var cartItem in cart.Items)
            {
                var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
                if (product == null)
                    throw new NotFoundException($"Product with ID {cartItem.ProductId} not found.");

                if (cartItem.Quantity > product.Stock)
                    throw new InvalidInputsException($"{product.Name} has only {product.Stock} available.");

                totalAmount += cartItem.Quantity * cartItem.SnapShotPrice;

                product.Stock -= cartItem.Quantity;
                product.SalesCount += cartItem.Quantity;
                await _productRepository.UpdateAsync(product);
            }

            var orderAddress = new Address
            {
                Id = Guid.NewGuid(),
                Governorate = governorate.Name,
                City = city.Name,
                Region = request.CreateOrderDto.Region.Trim(),
                UserId = userId
            };

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Status = OrderStatus.Pending,
                UserId = userId,
                PhoneNumber = request.CreateOrderDto.PhoneNumber,
                Address = orderAddress,
                Items = cart.Items.Select(ci => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    SnapShotPrice = ci.SnapShotPrice,
                }).ToList()
            };
            await _orderRepository.AddAsync(order);
            cart.Items.Clear();
            await _cartRepository.UpdateAsync(cart);
            var responseDto = _mapper.Map<OrderDto>(order);
            responseDto.UserName = userName;
            return responseDto;
        }
    }
}
