using Application.Interfaces.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NewsletterSubscriberRepository(ApplicationDbContext _context) : INewsletterSubscriberRepository
    {
        public async Task AddAsync(NewsletterSubscriber subscriber)
        {
            await _context.NewsletterSubscribers.AddAsync(subscriber);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponseModel<NewsletterSubscriber>> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.NewsletterSubscribers.CountAsync();
            var subscribers = await _context.NewsletterSubscribers
                .OrderByDescending(s => s.SubscribedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResponseModel<NewsletterSubscriber>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = subscribers
            };
        }

        public async Task<NewsletterSubscriber?> GetByEmailAsync(string email)
        {
            return await _context.NewsletterSubscribers
                         .FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.NewsletterSubscribers.CountAsync(ns => ns.IsActive);
        }

        public async Task UpdateAsync(NewsletterSubscriber subscriber)
        {
            _context.NewsletterSubscribers.Update(subscriber);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(NewsletterSubscriber subscriber)
        {
            _context.NewsletterSubscribers.Remove(subscriber);
            await _context.SaveChangesAsync();
        }
    }
}
