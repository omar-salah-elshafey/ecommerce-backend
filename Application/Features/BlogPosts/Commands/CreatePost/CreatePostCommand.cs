using Application.Features.BlogPosts.Dtos;
using MediatR;

namespace Application.Features.BlogPosts.Commands.CreatePost
{
    public record CreatePostCommand(CreatePostDto CreatePostDto) : IRequest<PostDto>;
}
