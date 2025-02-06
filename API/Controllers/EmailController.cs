using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController(IEmailService _emailService) : ControllerBase
    {
        [HttpPost("confirm-email")]
        public async Task<IActionResult> VerifyEmail(ConfirmEmailDto confirmEmailDto)
        {
            await _emailService.VerifyEmail(confirmEmailDto);

            return Ok(new { Message = "تم تفعيل حسابك بنجاح." });
        }

        [HttpPost("resend-confirmation-email/{Email}")]
        public async Task<IActionResult> ResendConfirmationEmail(string Email)
        {
            await _emailService.ResendEmailConfirmationTokenAsync(Email);

            return Ok(new { Message = "تم إرسال كود جديد لبريدك الإلكتروني." });
        }
    }
}
