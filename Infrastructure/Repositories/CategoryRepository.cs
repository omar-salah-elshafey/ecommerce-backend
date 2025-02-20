using Application.Interfaces.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository(ApplicationDbContext _context) : ICategoryRepository
    {
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var totalItems = await _context.Categories.CountAsync();
            return await _context.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.SubCategories)
                .Where(c => c.ParentCategoryId == null)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<bool> HasSubCategoriesAsync(Guid parentCategoryId)
        {
            return await _context.Categories
                .AnyAsync(c => c.ParentCategoryId == parentCategoryId);
        }

        public async Task<List<Category>> GetSubCategotires(Guid parentCategoryId)
        {
            return await _context.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.SubCategories)
                .Where(c => c.ParentCategoryId == parentCategoryId).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            category.IsDeleted = true;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<List<Category>> GetCategoriesByIdsAsync(List<Guid> categoryIds)
        {
            return await _context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();
        }
    }
}
