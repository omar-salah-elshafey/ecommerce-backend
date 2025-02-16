using Application.Interfaces.IRepositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDbContext _context) : IOrderRepository
    {
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatusAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetOrdersByUserNameAsync(string userName)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Include(o => o.User)
                .Where(o => o.User.UserName == userName)
                .ToListAsync();
        }

        public async Task<List<Order>> GetInProgressOrdersByUserNameAsync(string userName)
        {
            return await _context.Orders
                .Where(o => o.User.UserName == userName && o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled)
                .Include(o => o.User)
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllInProgressOrdersAsync()
        {
            return await _context.Orders
                .Where(o => o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled)
                .Include(o => o.User)
                .Include(o => o.Items)
                .ToListAsync();
        }
    }
}
