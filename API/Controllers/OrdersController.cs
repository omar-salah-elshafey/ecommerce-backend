using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetAllOrdersByUser;
using Application.Features.Orders.Queries.GetCitiesByGovernorate;
using Application.Features.Orders.Queries.GetGovernorates;
using Application.Features.Orders.Queries.GetInProgressOrders;
using Application.Features.Orders.Queries.GetInProgressOrdersByUser;
using Application.Features.TokenManagement.GetUsernameFromToken;
using Application.Interfaces.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator _mediator, ICityRepository _cityRepository, IGovernorateRepository _governorateRepository) : ControllerBase
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
        public async Task<IActionResult> PlaceOrderAsync([FromForm] CreateOrderDto orderDto)
        {
            var result = await _mediator.Send(new CreateOrderCommand(orderDto));
            return Ok(result);
        }

        [HttpGet("get-all-in-progress-orders")]
        public async Task<IActionResult> GetInProgressOrdersAsync()
        {
            var orders = await _mediator.Send(new GetInProgressOrdersQuery());
            return Ok(orders);
        }

        [HttpGet("get-all-orders-history")]
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
        public async Task<IActionResult> GetInProgressOrdersByUserAsync(string userName)
        {
            var orders = await _mediator.Send(new GetInProgressOrdersByUserQuery(userName));
            return Ok(orders);
        }

        [HttpGet("get-orders-history-by-user/{userName}")]
        public async Task<IActionResult> GetOrderHistoryByUserAsync(string userName)
        {
            var orders = await _mediator.Send(new GetAllOrdersByUserQuery(userName));
            return Ok(orders);
        }
    }
}
