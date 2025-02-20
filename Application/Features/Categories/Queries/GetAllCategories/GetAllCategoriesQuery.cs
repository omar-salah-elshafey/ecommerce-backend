using Application.Features.Categories.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery() : IRequest<List<CategoryDto>>;
}
