using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<PaginatedResponseModel<Product>> GetAllAsync(int pageNumber, int pageSize);
        Task<PaginatedResponseModel<Product>> GetFeaturedProductsAsync(int pageNumber, int pageSize);
        Task<List<Product>> GetBestSellerProductsAsync();
        Task<PaginatedResponseModel<Product>> GetByCategoryIdAsync(List<Guid> categoryIds, int pageNumber, int pageSize);
        Task<PaginatedResponseModel<Product>> GetByNameAsync(string query, int pageNumber, int pageSize);
        Task<int> GetCountAsync();
        Task AddAsync(Product product);
        Task AddProductImagesAsync(ProductImage productImage);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<bool> SkuExistsAsync(string sku);
    }
}
