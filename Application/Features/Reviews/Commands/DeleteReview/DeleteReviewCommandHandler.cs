using Application.ExceptionHandling;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Features.TokenManagement.GetUserRoleFromToken;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler(IReviewRepository _reviewRepository, IMediator _mediator) 
        : IRequestHandler<DeleteReviewCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(request.ReviewId);
            if (review == null)
                throw new NotFoundException("Review Not Found!");
            var role = await _mediator.Send(new GetUserRoleFromTokenQuery());
            var isAdmin = role.ToLower().Equals("admin");
            var currrentUserId = await _mediator.Send(new GetUserIdFromTokenQuery());
            if (!currrentUserId.Equals(review.UserId) && !isAdmin)
                throw new ForbiddenAccessException("لا يمكنك إتمام هذه العملية!");
            await _reviewRepository.DeleteReviewAsync(review);
            return Unit.Value;
        }
    }
}
