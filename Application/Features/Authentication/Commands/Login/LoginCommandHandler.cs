using Application.ExceptionHandling;
using Application.Features.Authentication.Commands.RegisterUser;
using Application.Features.Authentication.Dtos;
using Application.Features.TokenManagement.CreateToken;
using Application.Features.TokenManagement.GenerateRefreshToken;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler(UserManager<User> _userManager, ILogger<RegisterUserCommandHandler> _logger,
        IMediator _mediator, ICookieService _cookieService) : IRequestHandler<LoginCommand, AuthResponseModel>
    {
        public async Task<AuthResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting the Login Process....");
            var loginDto = request.loginDto;
            var authResponseModel = new AuthResponseModel();
            var user = await _userManager.FindByNameAsync(loginDto.EmailOrUserName.Trim())
               ?? await _userManager.FindByEmailAsync(loginDto.EmailOrUserName.Trim());
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password.Trim()))
                throw new InvalidCredentialsException("خطأ في اسم المستخدم أو كلمة المرور!");
            if (!user.EmailConfirmed)
                throw new EmailNotConfirmedException("برجاء تفعيل الحساب أولا.");
            var jwtSecurityToken = await _mediator.Send(new CreateTokenCommand(user));
            authResponseModel.Email = user.Email;
            authResponseModel.ExpiresAt = jwtSecurityToken.ValidTo.ToLocalTime();
            authResponseModel.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User";
            authResponseModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authResponseModel.Username = user.UserName;
            authResponseModel.IsConfirmed = true;
            user.Online = true;

            if (!user.RefreshTokens.Any(t => t.IsActive))
            {
                _logger.LogInformation("Generate a new refresh token and add it to the user's tokens...");

                var refreshToken = await _mediator.Send(new GenerateRefreshTokenCommand());
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);

                authResponseModel.RefreshToken = refreshToken.Token;
                authResponseModel.RefreshTokenExpiresOn = refreshToken.ExpiresOn.ToLocalTime();
                _cookieService.SetRefreshTokenCookie(refreshToken.Token, refreshToken.ExpiresOn);
            }
            else
            {
                _logger.LogInformation("Getting the active refreshToken...");
                var activeToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authResponseModel.RefreshToken = activeToken.Token;
                authResponseModel.RefreshTokenExpiresOn = activeToken.ExpiresOn.ToLocalTime();
                _cookieService.SetRefreshTokenCookie(activeToken.Token, activeToken.ExpiresOn);
            }
            _logger.LogInformation("User logged in..");

            return authResponseModel;
        }
    }
}
