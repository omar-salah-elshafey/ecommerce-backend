using Application.Interfaces.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository(ApplicationDbContext _context) : IProductRepository
    {
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PaginatedResponseModel<Product>> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalItems = await GetCountAsync();
            var products = await _context.Products
                .OrderByDescending(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(p => p.Images)
                .Include(p => p.Category)
                .ToListAsync();
            return new PaginatedResponseModel<Product>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = products
            };
        }

        public Task<int> GetCountAsync()
        {
            return _context.Products.CountAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Product product)
        {
            product.IsDeleted = true;
            await UpdateAsync(product);
        }

        public async Task<bool> SkuExistsAsync(string sku)
        {
            return await _context.Products.AnyAsync(p => p.SKU == sku);
        }
    }
}
