using Application.Features.Orders.Commands.CancelOrder;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Commands.UpdateOrderStatus;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetAllOrdersByUser;
using Application.Features.Orders.Queries.GetCitiesByGovernorate;
using Application.Features.Orders.Queries.GetGovernorates;
using Application.Features.Orders.Queries.GetInProgressOrders;
using Application.Features.Orders.Queries.GetInProgressOrdersByUser;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.TokenManagement.GetUsernameFromToken;
using Application.Interfaces.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("governorates")]
        public async Task<IActionResult> GetGovernoratesAsync()
        {
            var governorates = await _mediator.Send(new GetGovernoratesQuery());
            return Ok(governorates);
        }

        [HttpGet("governorates/{governorateId}/cities")]
        public async Task<IActionResult> GetCitiesByGovernorateAsync(Guid governorateId)
        {
            var cities = await _mediator.Send(new GetCitiesByGovernorateQuery(governorateId));
            if (cities == null || !cities.Any())
                return NotFound("No cities found for the given governorate.");

            return Ok(cities);
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrderAsync(CreateOrderDto orderDto)
        {
            var result = await _mediator.Send(new CreateOrderCommand(orderDto));
            return Ok(result);
        }

        [HttpGet("get-order-by-id/{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdCommand(id));
            return Ok(order);
        }

        [HttpGet("get-all-in-progress-orders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInProgressOrdersAsync()
        {
            var orders = await _mediator.Send(new GetInProgressOrdersQuery());
            return Ok(orders);
        }

        [HttpGet("get-all-orders-history")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderHistoryAsync()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(orders);
        }

        [HttpGet("get-current-user-in-progress-orders")]
        public async Task<IActionResult> GetCurrentUserInProgressOrdersAsync()
        {
            var userName = await _mediator.Send(new GetUsernameFromTokenQuery());
            var orders = await _mediator.Send(new GetInProgressOrdersByUserQuery(userName));
            return Ok(orders);
        }

        [HttpGet("get-current-user-orders-history")]
        public async Task<IActionResult> GetCurrentUserOrderHistoryAsync()
        {
            var userName = await _mediator.Send(new GetUsernameFromTokenQuery());
            var orders = await _mediator.Send(new GetAllOrdersByUserQuery(userName));
            return Ok(orders);
        }

        [HttpGet("get-in-progress-orders-by-user/{userName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInProgressOrdersByUserAsync(string userName)
        {
            var orders = await _mediator.Send(new GetInProgressOrdersByUserQuery(userName));
            return Ok(orders);
        }

        [HttpGet("get-orders-history-by-user/{userName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderHistoryByUserAsync(string userName)
        {
            var orders = await _mediator.Send(new GetAllOrdersByUserQuery(userName));
            return Ok(orders);
        }

        [HttpPut("update-order-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrderStatusDto)
        {
            var updatedOrder = await _mediator.Send(new UpdateOrderStatusCommand(updateOrderStatusDto));
            return Ok(updatedOrder);
        }

        [HttpPut("cancel-order/{orderId}")]
        public async Task<IActionResult> CancelOrderAsync(Guid orderId)
        {
            var cancelledOrder = await _mediator.Send(new CancelOrderCommand(orderId));
            return Ok(cancelledOrder);
        }
    }
}
