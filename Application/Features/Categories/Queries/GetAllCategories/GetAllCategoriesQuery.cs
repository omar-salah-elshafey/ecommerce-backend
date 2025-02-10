using Application.Features.Categories.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<CategoryDto>>;
}
