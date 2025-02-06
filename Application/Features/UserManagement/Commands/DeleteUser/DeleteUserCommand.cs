using MediatR;

namespace Application.Features.UserManagement.Commands.DeleteUser
{
    public record DeleteUserCommand(string UserName, string refreshToken) : IRequest;
}
