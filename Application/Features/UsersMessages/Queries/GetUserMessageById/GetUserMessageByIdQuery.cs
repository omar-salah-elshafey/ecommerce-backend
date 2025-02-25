using Application.Features.UsersMessages.Dtos;
using MediatR;

namespace Application.Features.UsersMessages.Queries.GetUserMessageById
{
    public record GetUserMessageByIdQuery(Guid Id) : IRequest<MessageDto>;
}
