using Application.Features.PasswordManagement.Dtos;
using MediatR;

namespace Application.Features.PasswordManagement.Commands.ResetPassword
{
    public record ResetPasswordCommand(ResetPasswordDto resetPasswordDto) : IRequest;
}
