using Application.ExceptionHandling;
using Application.Features.UsersMessages.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.UsersMessages.Commands.UpdateMessageStatus
{
    public class UpdateMessageStatusCommandHandler(IUsersMessagesRepository _usersMessagesRepository, IMapper _mapper)
        : IRequestHandler<UpdateMessageStatusCommand, MessageDto>
    {
        public async Task<MessageDto> Handle(UpdateMessageStatusCommand request, CancellationToken cancellationToken)
        {
            var message = await _usersMessagesRepository.GetMessageAsync(request.Id);
            if (message is null)
                throw new NotFoundException("NOT FOUND!");
            message.IsRead = !message.IsRead;
            await _usersMessagesRepository.UpdateMessageStatusAsync(message);
            return _mapper.Map<MessageDto>(message);
        }
    }
}
