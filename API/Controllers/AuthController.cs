﻿using Application.ExceptionHandling;
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
    public class AuthController(IMediator _mediator, ICookieService _cookieService, ILogger<AuthController> _logger) : ControllerBase
    {
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrationDto registrationDto)
        {
            if (registrationDto.Role == Role.Admin)
                throw new ForbiddenAccessException("لا يمكن إتمام العملية!.");

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
        [Authorize(Roles ="Admin")]
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
            return Ok(new {message = "تم تسجيل الخروج بنجاح." });
        }

        [HttpGet("refreshtoken")]
        public async Task<IActionResult> RefreshToken(string? refreshToken)
        {
            _logger.LogInformation("refreshToken from the frontend: " + refreshToken);
            if (refreshToken is null)
            {
                refreshToken = Request.Cookies["refreshToken"];
                _logger.LogInformation("refreshToken from the backend: " + refreshToken);
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
