using Application.Features.BlogPosts.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.BlogPosts.Queries.GetAllPosts
{
    public record GetAllPostsQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<PostDto>>;
}
