using Application.Features.Categories.Dtos;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;
}
