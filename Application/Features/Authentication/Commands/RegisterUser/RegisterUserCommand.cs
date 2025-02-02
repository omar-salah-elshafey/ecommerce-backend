using Application.Features.Authentication.Dtos;
using MediatR;

namespace Application.Features.Authentication.Commands.RegisterUser
{
    public record RegisterUserCommand(RegistrationDto registrationDto) : IRequest<AuthResponseModel>;
}
