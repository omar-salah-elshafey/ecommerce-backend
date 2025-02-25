using Application.Features.UsersMessages.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.UsersMessages.Queries.GetUsersMessages
{
    public record GetUsersMessagesQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<MessageDto>>;
}
