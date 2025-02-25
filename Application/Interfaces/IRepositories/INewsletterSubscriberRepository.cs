using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface INewsletterSubscriberRepository
    {
        Task AddAsync(NewsletterSubscriber subscriber);
        Task<PaginatedResponseModel<NewsletterSubscriber>> GetAllAsync(int pageNumber, int pageSize);
        Task<NewsletterSubscriber?> GetByEmailAsync(string email);
        Task UpdateAsync(NewsletterSubscriber subscriber);
        Task DeleteAsync(NewsletterSubscriber subscriber);
    }
}
