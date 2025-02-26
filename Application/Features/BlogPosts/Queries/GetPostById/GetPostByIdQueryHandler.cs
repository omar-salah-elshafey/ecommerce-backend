using Application.ExceptionHandling;
using Application.Features.BlogPosts.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.BlogPosts.Queries.GetPostById
{
    public class GetPostByIdQueryHandler(IPostRepository _postRepository, IMapper _mapper)
        : IRequestHandler<GetPostByIdQuery, PostDto>
    {
        public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.Id);
            if (post is null)
                throw new NotFoundException("Post not Found");
            return _mapper.Map<PostDto>(post);
        }
    }
}
