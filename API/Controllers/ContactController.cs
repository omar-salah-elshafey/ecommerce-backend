using Application.Features.UsersMessages.Commands.CreateContactMessage;
using Application.Features.UsersMessages.Commands.DeleteMessage;
using Application.Features.UsersMessages.Commands.UpdateMessageStatus;
using Application.Features.UsersMessages.Dtos;
using Application.Features.UsersMessages.Queries.GetUserMessageById;
using Application.Features.UsersMessages.Queries.GetUsersMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("messages/send-message")]
        public async Task<IActionResult> SendMessageAsync(SendMessageDto dto)
        {
            await _mediator.Send(new CreateContactMessageCommand(dto));
            return Created();
        }

        [HttpGet("messages")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMessagesAsync(int PageNumber = 1, int PageSize = 10)
        {
            var result = await _mediator.Send(new GetUsersMessagesQuery(PageNumber, PageSize));
            return Ok(result);
        }

        [HttpGet("messages/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMessageByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetUserMessageByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("messages/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMessageStatusAsync(Guid id)
        {
            var result = await _mediator.Send(new UpdateMessageStatusCommand(id));
            return Ok(result);
        }

        [HttpDelete("messages/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMessageAsync(Guid id)
        {
            var result = await _mediator.Send(new DeleteMessageCommand(id));
            return NoContent();
        }
    }
}
