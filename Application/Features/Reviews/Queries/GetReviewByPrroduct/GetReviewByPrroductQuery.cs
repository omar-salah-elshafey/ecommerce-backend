using Application.Features.Reviews.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Reviews.Queries.GetReviewByPrroduct
{
    public record GetReviewByPrroductQuery(Guid ProductId, int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<ReviewDto>>;
}
