using Application.Features.Reviews.Dtos;
using MediatR;

namespace Application.Features.Reviews.Queries.GetReviewById
{
    public record GetReviewByIdQuery(Guid Id) : IRequest<ReviewDto>;
}
