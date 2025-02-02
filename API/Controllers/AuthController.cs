using Application.ExceptionHandling;
using Application.Features.Authentication.Commands.Login;
using Application.Features.Authentication.Commands.Logout;
using Application.Features.Authentication.Commands.RegisterUser;
using Application.Features.Authentication.Dtos;
using Application.Features.TokenManagement.RefreshTokens;
using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICookieService _cookieService;

        public AuthController(IMediator mediator, ICookieService cookieService)
        {
            _mediator = mediator;
            _cookieService = cookieService;
        }
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrationDto registrationDto)
        {
            if (registrationDto.Role == Role.Admin)
                throw new ForbiddenAccessException("You cannot register as an admin.");

            var result = await _mediator.Send(new RegisterUserCommand(registrationDto));

            return Ok(new
            {
                result.Email,
                result.Username,
                result.Role,
                result.IsConfirmed,
            });
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUserAsync([FromBody] RegistrationDto registrationDto)
        {
            var result = await _mediator.Send(new RegisterUserCommand(registrationDto));

            return Ok(new
            {
                result.Email,
                result.Username,
                result.Role,
                result.IsConfirmed,
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var result = await _mediator.Send(new LoginCommand(loginDto));

            return Ok(new
            {
                result.AccessToken,
                result.ExpiresAt,
                result.RefreshToken,
                result.RefreshTokenExpiresOn,
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout(string? refreshToken)
        {
            if (refreshToken is null)
            {
                refreshToken = Request.Cookies["refreshToken"];
            }
            await _mediator.Send(new LogoutCommand(refreshToken));
            return Ok(new {message = "Successfully logged out" });
        }

        [HttpGet("refreshtoken")]
        public async Task<IActionResult> RefreshToken(string? refreshToken)
        {
            if (refreshToken is null)
            {
                refreshToken = Request.Cookies["refreshToken"];
            }

            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));

            _cookieService.SetRefreshTokenCookie(result.RefreshToken, result.RefreshTokenExpiresOn);
            return Ok(new
            {
                result.AccessToken,
                result.ExpiresAt,
                result.RefreshToken,
                result.RefreshTokenExpiresOn,
            });
        }
    }
}
