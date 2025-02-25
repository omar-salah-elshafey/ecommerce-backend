using MediatR;

namespace Application.Features.UsersMessages.Commands.DeleteMessage
{
    public record DeleteMessageCommand(Guid Id) : IRequest<Unit>;
}
