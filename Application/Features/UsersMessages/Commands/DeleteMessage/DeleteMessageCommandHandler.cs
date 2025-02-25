using Application.ExceptionHandling;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.UsersMessages.Commands.DeleteMessage
{
    public class DeleteMessageCommandHandler(IUsersMessagesRepository _usersMessagesRepository)
        : IRequestHandler<DeleteMessageCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _usersMessagesRepository.GetMessageAsync(request.Id);
            if(message is null)
                throw new NotFoundException("NOT FOUND!");
            await _usersMessagesRepository.DeleteMessageAsync(message);
            return Unit.Value;
        }
    }
}
