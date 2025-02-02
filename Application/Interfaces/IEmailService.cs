using Application.Dtos;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task VerifyEmail(ConfirmEmailDto confirmEmailDto);
        Task ResendEmailConfirmationTokenAsync(string UserName);
    }
}
