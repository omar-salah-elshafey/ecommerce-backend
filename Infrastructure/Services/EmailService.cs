using Application.Dtos;
using Application.ExceptionHandling;
using Application.Features.OTPs.Commands.GenerateAndStoreOtp;
using Application.Features.OTPs.Queries.GetTokenFromOtp;
using Application.Interfaces;
using Domain.Entities;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailService(IConfiguration _config, UserManager<User> _userManager, ILogger<EmailService> _logger, IMediator _mediator) : IEmailService
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

            var (realToken, isExpired) = await _mediator.Send( new GetTokenFromOtpQuery(confirmEmailDto.Email, confirmEmailDto.Token));
            if (realToken == null)
            {
                if (isExpired)
                {
                    _logger.LogWarning("OTP has expired for user {Email}", user.Email);
                    throw new InvalidTokenException("انتهت صلاحية الرمز، من فضلك اطلب رمز جديد");
                }
                _logger.LogError("Invalid OTP for user {Email}", user.Email);
                throw new InvalidTokenException("الرمز غير صالح.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, realToken);
            if (!result.Succeeded)
                throw new InvalidTokenException("الرمز غير صالح.");

            _logger.LogInformation("Email confirmed successfully for user {Email}", user.Email);
        }

        public async Task ResendEmailConfirmationTokenAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                throw new NotFoundException("البريد الإلكتروني غير صالح!");

            if (await _userManager.IsEmailConfirmedAsync(user))
                throw new EmailAlreadyConfirmedException("الحساب مفعل بالفعل.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var otp = await _mediator.Send(new GenerateAndStoreOtpCommand(user.Email, token));
            await SendEmailAsync(user.Email, "رمز التحقق من البريد الإلكتروني",
                $"أهلا {user.UserName}, استخدم هذا الرمز للتحقق من بريدك الإلكتروني: {otp}\n الرمز صالح لمدة 10 دقائق فقط.");
            _logger.LogInformation("New OTP sent to {Email}", user.Email);
        }
    }
}
