using Application.Features.BlogPosts.Dtos;
using MediatR;

namespace Application.Features.BlogPosts.Commands.UpdatePost
{
    public record UpdatePostCommand(Guid Id, UpdatePostDto UpdatePostDto) : IRequest<PostDto>;
}
