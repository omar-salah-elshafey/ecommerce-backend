using Application.Dtos;
using MediatR;

namespace Application.Features.PasswordManagement.Commands.VerifyResetPasswordRequest
{
    public record VerifyResetPasswordRequestCommand(ConfirmEmailDto ConfirmEmailDto) : IRequest;
}
