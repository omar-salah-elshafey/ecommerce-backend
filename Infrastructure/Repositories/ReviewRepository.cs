using Application.Interfaces.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReviewRepository(ApplicationDbContext _context) : IReviewRepository
    {
        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Reviews.CountAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(Guid id)
        {
            return await _context.Reviews.Include(r => r.User).Include(r => r.Product).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> UserHasReview(string userId, Guid productId)
        {
            return await _context.Reviews.AnyAsync(r => r.UserId == userId && r.ProductId == productId);
        }

        public async Task<PaginatedResponseModel<Review>> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalItems = await GetCountAsync();
            var reviews = await _context.Reviews
                .OrderByDescending(r => r.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(r => r.Product)
                .Include(r => r.User)
                .ToListAsync();
            return new PaginatedResponseModel<Review>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = reviews
            };
        }

        public async Task<PaginatedResponseModel<Review>> GetReviewsByProductIdAsync(Guid productId, int pageNumber, int pageSize)
        {
            var totalItems = await _context.Reviews.CountAsync(r => r.ProductId == productId);
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(r => r.Product)
                .Include(r => r.User)
                .ToListAsync();
            return new PaginatedResponseModel<Review>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = reviews
            };
        }

        public async Task UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(Review review)
        {
            review.IsDeleted = true;
            await UpdateReviewAsync(review);
        }
    }
}
