using Application.Interfaces.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Infrastructure.Repositories
{
    public class CategoryRepository(ApplicationDbContext _context) : ICategoryRepository
    {
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<PaginatedResponseModel<Category>> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Categories.CountAsync();
            var categoties = await _context.Categories
                .OrderByDescending(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResponseModel<Category>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = categoties
            };
        }

        public async Task<bool> HasSubCategoriesAsync(Guid parentCategoryId)
        {
            return await _context.Categories
                .AnyAsync(c => c.ParentCategoryId == parentCategoryId);
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
