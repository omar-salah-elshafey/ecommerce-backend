using Application.Features.UsersMessages.Dtos;
using MediatR;

namespace Application.Features.UsersMessages.Commands.UpdateMessageStatus
{
    public record UpdateMessageStatusCommand(Guid Id) : IRequest<MessageDto>;
}
