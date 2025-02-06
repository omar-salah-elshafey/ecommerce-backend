using Application.Dtos;
using Application.Features.PasswordManagement.Commands.ChangePassword;
using Application.Features.PasswordManagement.Commands.ResetPassword;
using Application.Features.PasswordManagement.Commands.ResetPasswordRequest;
using Application.Features.PasswordManagement.Commands.VerifyResetPasswordRequest;
using Application.Features.PasswordManagement.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordManagerController(IMediator _mediator) : ControllerBase
    {
        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            await _mediator.Send(new ChangePasswordCommand(changePasswordDto));
            return Ok(new { Message = "تم تغيير كلمة المرور بنجاح." });
        }

        [HttpPost("reset-password-request/{email}")]
        public async Task<IActionResult> ResetPasswordRequestAsync(string email)
        {
            await _mediator.Send(new ResetPasswordRequestCommand(email));
            return Ok(new { Message = "تم إرسال رمز إعادة تعيين كلمة السر لبريدك الإلكتروني." });
        }

        [HttpPost("verify-password-reset-token")]
        public async Task<IActionResult> VerifyResetPasswordRequestAsync(ConfirmEmailDto confirmEmailDto)
        {
            await _mediator.Send(new VerifyResetPasswordRequestCommand(confirmEmailDto));
            return Ok(new { Message = "تم التحقق من الرمز بنجاح." });
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            await _mediator.Send(new ResetPasswordCommand(resetPasswordDto));
            return Ok(new { Message = "تم إعادة تعيين كلمة المرور بنجاح." });
        }
    }
}
