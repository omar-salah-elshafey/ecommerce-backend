using Application.Features.Wishlists.Commands.AddWishlistItem;
using Application.Features.Wishlists.Commands.RemoveWishlistItem;
using Application.Features.Wishlists.Queries.GetWishlist;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishlistController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("add-to-wishlist/{productId}")]
        public async Task<IActionResult> AddWishlistItem(Guid productId)
        {
            var result = await _mediator.Send(new AddWishlistItemCommand(productId));
            return Ok(result);
        }

        [HttpGet("get-wishlist")]
        public async Task<IActionResult> GetWishlist()
        {
            var result = await _mediator.Send(new GetWishlistQuery());
            return Ok(result);
        }

        [HttpDelete("remove-wishlist-item/{productId}")]
        public async Task<IActionResult> RemoveWishlistItem(Guid productId)
        {
            var result = await _mediator.Send(new RemoveWishlistItemCommand(productId));
            return Ok(result);
        }
    }
}
