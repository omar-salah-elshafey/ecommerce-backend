using Application.Features.Categories.Dtos;
using MediatR;

namespace Application.Features.Categories.Queries.GetSubCategories
{
    public record GetSubCategoriesQuery(Guid ParentCategoryId) : IRequest<List<CategoryDto>>;
}
