using Application.Features.Reviews.Dtos;
using MediatR;

namespace Application.Features.Reviews.Commands.AddReview
{
    public record AddReviewCommand(CreateReviewDto CreateReviewDto) : IRequest<ReviewDto>;
}
