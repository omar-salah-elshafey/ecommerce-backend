using Application.Features.Authentication.Commands.RegisterUser;
using Application.Features.Authentication.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(new RegisterUserCommand(registrationDto));


            return Ok(new
            {
                result.Email,
                result.Username,
                result.Role,
                result.IsConfirmed,
            });
        }
    }
}
