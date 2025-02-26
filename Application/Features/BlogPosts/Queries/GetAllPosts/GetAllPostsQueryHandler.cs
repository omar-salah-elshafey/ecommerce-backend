using Application.Features.BlogPosts.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.BlogPosts.Queries.GetAllPosts
{
    public class GetAllPostsQueryHandler(IPostRepository _postRepository, IMapper _mapper)
        : IRequestHandler<GetAllPostsQuery, PaginatedResponseModel<PostDto>>
    {
        public async Task<PaginatedResponseModel<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllAsync(request.PageNumber, request.PageSize);
            return new PaginatedResponseModel<PostDto>
            {
                TotalItems = posts.TotalItems,
                PageNumber = posts.PageNumber,
                PageSize = posts.PageSize,
                Items = _mapper.Map<IEnumerable<PostDto>>(posts.Items)
            };
        }
    }
}
