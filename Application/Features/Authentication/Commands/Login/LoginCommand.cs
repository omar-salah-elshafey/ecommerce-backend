using Application.Features.Authentication.Dtos;
using MediatR;

namespace Application.Features.Authentication.Commands.Login
{
    public record LoginCommand(LoginDto loginDto) : IRequest<AuthResponseModel>;
}
