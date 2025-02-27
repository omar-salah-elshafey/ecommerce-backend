using MediatR;

namespace Application.Features.Categories.Queries.GetCategoriesCount
{
    public record GetCategoriesCountQuery : IRequest<int>;
}
