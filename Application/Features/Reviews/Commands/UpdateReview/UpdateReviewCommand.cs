using Application.Features.Reviews.Dtos;
using MediatR;

namespace Application.Features.Reviews.Commands.UpdateReview
{
    public record UpdateReviewCommand(Guid ReviewId, UpdateReviewDto UpdateReview) : IRequest<ReviewDto>;
}
