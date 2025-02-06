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
    public class EmailService(IConfiguration _config, UserManager<User> _userManager,
        IOptions<DataProtectionTokenProviderOptions> _tokenProviderOptions, ILogger<EmailService> _logger) : IEmailService
    {
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
                throw new InvalidEmailOrTokenException("البريد والرمز مطلوبان.");

            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user == null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");
            if (user.EmailConfirmed)
                throw new EmailAlreadyConfirmedException("الحساب مفعل بالفعل");
            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (!result.Succeeded)
                throw new InvalidTokenException("الرمز غير صالح.");

            _logger.LogInformation("Your email has been confirmed.");
        }

        public async Task ResendEmailConfirmationTokenAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");

            if (await _userManager.IsEmailConfirmedAsync(user))
                throw new EmailAlreadyConfirmedException("الحساب مفعل بالفعل.");

            // Generate new token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var expirationTime = _tokenProviderOptions.Value.TokenLifespan.TotalMinutes;
            // Send the new token via email
            await SendEmailAsync(user.Email, "رمز التحقق من البريد الإلكتروني",
                $"أهلا {user.UserName}, استخدم هذا الرمز للتحقق من بريدك الإلكتروني: {token}\n الرمز صالح لمدة {expirationTime} دقائق فقط.");
        }
    }
}
