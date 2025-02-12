using Application.Features.Carts.Commands.ChangeCartItem;
using Application.Features.Carts.Commands.RemoveCartItem;
using Application.Features.Carts.Dtos;
using Application.Features.Carts.Queries.GetCart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddCartItemAsync(CartItemChangeDto cartItemChangeDto)
        {
            var result = await _mediator.Send(new ChangeCartItemCommand(cartItemChangeDto));
            return Ok(result);
        }

        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCartAsync()
        {
            var result = await _mediator.Send(new GetCartQuery());
            return Ok(result);
        }

        [HttpDelete("remove-from-cart/{productId}")]
        public async Task<IActionResult> RemoveCartItemAsync(Guid productId)
        {
            var result = await _mediator.Send(new RemoveCartItemCommand(productId));
            return Ok(result);
        }
    }
}
