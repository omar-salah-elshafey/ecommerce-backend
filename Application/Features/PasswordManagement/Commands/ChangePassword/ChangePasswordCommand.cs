using Application.Features.PasswordManagement.Dtos;
using MediatR;

namespace Application.Features.PasswordManagement.Commands.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordDto changePasswordDto) : IRequest;
}
