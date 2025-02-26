using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IPostRepository
    {
        Task AddAsync(BlogPost post);
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<PaginatedResponseModel<BlogPost>> GetAllAsync(int pageNumber, int pageSize);
        Task UpdateAsync(BlogPost post);
    }
}
