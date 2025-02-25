using Application.Features.UsersMessages.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.UsersMessages.Queries.GetUsersMessages
{
    public class GetUsersMessagesQueryHandler(IUsersMessagesRepository _usersMessagesRepository, IMapper _mapper) 
        : IRequestHandler<GetUsersMessagesQuery, PaginatedResponseModel<MessageDto>>
    {
        public async Task<PaginatedResponseModel<MessageDto>> Handle(GetUsersMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _usersMessagesRepository.GetMessagesAsync(request.PageNumber, request.PageSize);
            return new PaginatedResponseModel<MessageDto> 
            {
                TotalItems = messages.TotalItems,
                PageNumber = messages.PageNumber,
                PageSize = messages.PageSize,
                Items = _mapper.Map<List<MessageDto>>(messages.Items)
            };
        }
    }
}
