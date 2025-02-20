using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<List<Category>> GetAllAsync();
        Task<List<Category>> GetSubCategotires(Guid parentCategoryId);
        Task<bool> HasSubCategoriesAsync(Guid parentCategoryId);
        Task<int> GetCountAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<List<Category>> GetCategoriesByIdsAsync(List<Guid> categoryIds);
    }
}
