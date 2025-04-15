using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderNotificationRepository(ApplicationDbContext _context) : IOrderNotificationRepository
    {

        public async Task AddAsync(OrderNotification notification)
        {
            await _context.OrderNotifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderNotification?> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderNotifications
                .Include(n => n.Order)
                .FirstOrDefaultAsync(n => n.OrderId == orderId);
        }

        public async Task UpdateAsync(OrderNotification notification)
        {
            _context.OrderNotifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderNotification>> GetUnprocessedAsync()
        {
            return await _context.OrderNotifications
                .Where(n => !n.IsProcessed)
                .Include(n => n.Order)
                .ToListAsync();
        }
    }
}
