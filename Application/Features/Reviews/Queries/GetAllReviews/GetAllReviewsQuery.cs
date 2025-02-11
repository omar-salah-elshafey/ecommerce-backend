using Application.Features.Reviews.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Reviews.Queries.GetAllReviews
{
    public record GetAllReviewsQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<ReviewDto>>;
}
