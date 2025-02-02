using Application.Dtos;
using Application.ExceptionHandling;
using Application.Interfaces;
using Domain.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<DataProtectionTokenProviderOptions> _tokenProviderOptions;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, UserManager<User> userManager,
            IOptions<DataProtectionTokenProviderOptions> tokenProviderOptions)
        {
            _config = config;
            _userManager = userManager;
            _tokenProviderOptions = tokenProviderOptions;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]),
                MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task VerifyEmail(ConfirmEmailDto confirmEmailDto)
        {

            if (string.IsNullOrEmpty(confirmEmailDto.Email) || string.IsNullOrEmpty(confirmEmailDto.Token))
            {
                _logger.LogError("Email and token are required.");
                throw new InvalidEmailOrTokenException("Email and token are required.");
            }

            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user == null)
            {
                _logger.LogError("User not found.");
                throw new NotFoundException("User not found.");
            }
            if (user.EmailConfirmed)
            {
                _logger.LogWarning("Your email is already confirmed.");
                throw new EmailAlreadyConfirmedException("Your Email is already confirmed");
            }
            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (!result.Succeeded)
            {
                _logger.LogError("Token is not valid.");
                throw new InvalidTokenException("Token is not valid.");
            }

            _logger.LogInformation("Your email has been confirmed.");
        }

        public async Task ResendEmailConfirmationTokenAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                _logger.LogError("User not found.");
                throw new NotFoundException("User not found.");
            }

            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                _logger.LogWarning("Your email is already confirmed.");
                throw new EmailAlreadyConfirmedException("Email is already confirmed.");
            }

            // Generate new token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var expirationTime = _tokenProviderOptions.Value.TokenLifespan.TotalMinutes;
            // Send the new token via email
            await SendEmailAsync(user.Email, "Email Verification Code",
                $"Hello {user.UserName}, Use this new token to verify your Email: {token}\n This code is Valid only for {expirationTime} Minutes.");
        }
    }
}
