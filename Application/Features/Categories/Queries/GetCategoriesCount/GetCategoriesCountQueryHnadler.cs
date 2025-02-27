using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoriesCount
{
    public class GetCategoriesCountQueryHnadler(ICategoryRepository _categoryRepository) : IRequestHandler<GetCategoriesCountQuery, int>
    {
        public async Task<int> Handle(GetCategoriesCountQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetCountAsync();
        }
    }
}
