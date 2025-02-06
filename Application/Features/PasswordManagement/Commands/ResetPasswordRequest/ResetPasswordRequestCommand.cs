using MediatR;

namespace Application.Features.PasswordManagement.Commands.ResetPasswordRequest
{
    public record ResetPasswordRequestCommand(string email) : IRequest;
}
