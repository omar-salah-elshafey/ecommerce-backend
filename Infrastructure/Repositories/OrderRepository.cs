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
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Images)
                .Include(o => o.User)
                .Include(o => o.Address)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetOrdersByUserNameAsync(string userName)
        {
            return await _context.Orders
                .Where(o => o.User.UserName == userName)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Images)
                .Include(o => o.User)
                .Include(o => o.Address)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetInProgressOrdersByUserNameAsync(string userName)
        {
            return await _context.Orders
                .Where(o => o.User.UserName == userName && o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled)
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Images)
                .Include(o => o.Address)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Images)
                .Include(o => o.Address)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllInProgressOrdersAsync()
        {
            return await _context.Orders
                .Where(o => o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled)
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Images)
                .Include(o => o.Address)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
