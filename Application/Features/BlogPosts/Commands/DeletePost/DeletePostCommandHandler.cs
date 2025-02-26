using Application.ExceptionHandling;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.BlogPosts.Commands.DeletePost
{
    public class DeletePostCommandHandler(IPostRepository _postRepository) : IRequestHandler<DeletePostCommand, Unit>
    {
        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.Id);
            if (post is null)
                throw new NotFoundException("Post not Found");
            post.IsDeleted = true;
            await _postRepository.UpdateAsync(post);
            return Unit.Value;
        }
    }
}
