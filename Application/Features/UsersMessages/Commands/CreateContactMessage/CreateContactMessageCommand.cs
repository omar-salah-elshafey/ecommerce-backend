using Application.Features.UsersMessages.Dtos;
using MediatR;

namespace Application.Features.UsersMessages.Commands.CreateContactMessage
{
    public record CreateContactMessageCommand(SendMessageDto Dto) : IRequest;
}
