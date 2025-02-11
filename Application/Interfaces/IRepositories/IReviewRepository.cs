using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IReviewRepository
    {
        Task AddReviewAsync(Review review);
        Task<Review?> GetReviewByIdAsync(Guid id);
        Task<bool> UserHasReview(string userId, Guid productId);
        Task<int> GetCountAsync();
        Task<PaginatedResponseModel<Review>> GetAllAsync(int pageNumber, int pageSize);
        Task<PaginatedResponseModel<Review>> GetReviewsByProductIdAsync(Guid productId, int pageNumber, int pageSize);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Review review);
    }
}
