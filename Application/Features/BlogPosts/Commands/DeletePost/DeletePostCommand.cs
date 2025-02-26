using MediatR;

namespace Application.Features.BlogPosts.Commands.DeletePost
{
    public record DeletePostCommand(Guid Id) : IRequest<Unit>;
}
