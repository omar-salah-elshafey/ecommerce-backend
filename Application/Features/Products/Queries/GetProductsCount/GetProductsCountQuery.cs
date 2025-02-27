using MediatR;

namespace Application.Features.Products.Queries.GetProductsCount
{
    public record GetProductsCountQuery: IRequest<int>;
}
