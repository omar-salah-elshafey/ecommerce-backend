using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<PaginatedResponseModel<Category>> GetAllAsync(int pageNumber, int pageSize);
        Task<bool> HasSubCategoriesAsync(Guid parentCategoryId);
        Task<int> GetCountAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
