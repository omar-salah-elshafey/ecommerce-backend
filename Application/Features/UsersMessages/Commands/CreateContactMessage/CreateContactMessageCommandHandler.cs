using Application.Features.UsersMessages.Dtos;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.UsersMessages.Commands.CreateContactMessage
{
    public class CreateContactMessageCommandHandler(IUsersMessagesRepository _usersMessagesRepository, IValidator<SendMessageDto> _validator)
        : IRequestHandler<CreateContactMessageCommand>
    {
        public async Task Handle(CreateContactMessageCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var message = new UsersMessage
            {
                Id = Guid.NewGuid(),
                Name = dto.FullName,
                Email = dto.Email,
                Message = dto.Message,
                MessageDate = DateTime.UtcNow
            };
            await _usersMessagesRepository.CreateMessageAsync(message);
        }
    }
}
