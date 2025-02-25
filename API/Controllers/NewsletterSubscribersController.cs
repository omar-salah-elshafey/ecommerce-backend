using Application.Features.NewsletterSubscribers.Commands.RemoveSubscriber;
using Application.Features.NewsletterSubscribers.Commands.Subscirbe;
using Application.Features.NewsletterSubscribers.Commands.UnSubscribe;
using Application.Features.NewsletterSubscribers.Queries.GetAllSubscribers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterSubscribersController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("subscribe/{email}")]
        public async Task<IActionResult> SubscribeAsync(string email)
        {
            var result = await _mediator.Send(new SubscirbeCommand(email));
            return Ok(result);
        }

        [HttpGet("subscribers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSubscribersAsync(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllSubscribersQuery(pageNumber, pageSize));
            return Ok(result);
        }

        [HttpPut("unsubscribe/{email}")]
        public async Task<IActionResult> UnSubscribeAsync(string email)
        {
            var result = await _mediator.Send(new UnSubscribeCommand(email));
            return Ok(result);
        }

        [HttpDelete("remove/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveSubscriberAsync(string email)
        {
            await _mediator.Send(new RemoveSubscriberCommand(email));
            return NoContent();
        }
    }
}
