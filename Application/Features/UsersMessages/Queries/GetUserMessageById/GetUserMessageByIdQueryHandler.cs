using Application.Features.UsersMessages.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.UsersMessages.Queries.GetUserMessageById
{
    public class GetUserMessageByIdQueryHandler(IUsersMessagesRepository _usersMessagesRepository, IMapper _mapper)
        : IRequestHandler<GetUserMessageByIdQuery, MessageDto>
    {
        public async Task<MessageDto> Handle(GetUserMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var message = await _usersMessagesRepository.GetMessageAsync(request.Id);
            return _mapper.Map<MessageDto>(message);
        }
    }
}
