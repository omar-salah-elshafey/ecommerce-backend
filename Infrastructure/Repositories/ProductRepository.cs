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
                .OrderByDescending(p => p.CreatedAt)
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
        public async Task<PaginatedResponseModel<Product>> GetFeaturedProductsAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Products.CountAsync(p => p.IsFeatured);
            var products = await _context.Products
                .Where(p => p.IsFeatured)
                .OrderByDescending(p => p.CreatedAt)
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
        public async Task<List<Product>> GetBestSellerProductsAsync()
        {
            var products = await _context.Products
                .Where(p => p.SalesCount > 0)
                .OrderByDescending(p => p.SalesCount)
                .Take(20)
                .Include(p => p.Images)
                .Include(p => p.Category)
                .ToListAsync();
            return products;
        }

        public async Task<PaginatedResponseModel<Product>> GetByCategoryIdAsync(Guid categoryId, int pageNumber, int pageSize)
        {
            var totalItems = await _context.Products.CountAsync(p => p.CategoryId == categoryId);
            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .OrderByDescending(p => p.CreatedAt)
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

        public async Task<PaginatedResponseModel<Product>> GetByNameAsync(string query, int pageNumber, int pageSize)
        {
            var totalItems = await _context.Products.CountAsync(p => p.Name.ToLower().Contains(query.ToLower()));
            var products = await _context.Products
                .Where(p => p.Name.ToLower().Contains(query.ToLower()))
                .OrderByDescending(p => p.CreatedAt)
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

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddProductImagesAsync(ProductImage productImage)
        {
            await _context.ProductImages.AddAsync(productImage);
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
