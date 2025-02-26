using Application.Features.BlogPosts.Dtos;
using MediatR;

namespace Application.Features.BlogPosts.Queries.GetPostById
{
    public record GetPostByIdQuery(Guid Id) : IRequest<PostDto>;
}
