using Application.Interfaces.IRepositories;
using Application.Models;
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

        public async Task<List<Address>> GetUserAddressesAsync(string userId)
        {
            return await _context.Addresses.Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<Address?> GetAddressByIdAsync(Guid addressId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
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

        public async Task<PaginatedResponseModel<Order>> GetAllOrdersAsync(int pageNumber, int pageSize)
        {
            var totalItems = await GetOrdersCountAsync();
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Images)
                .Include(o => o.Address)
                .OrderByDescending(o => o.OrderDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResponseModel<Order>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = orders
            };
        }

        public async Task<PaginatedResponseModel<Order>> GetAllInProgressOrdersAsync(int pageNumber, int pageSize)
        {
            var totalItems = await GetOrdersCountAsync();
            var orders = await _context.Orders
                .Where(o => o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled)
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Images)
                .Include(o => o.Address)
                .OrderByDescending(o => o.OrderDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResponseModel<Order>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = orders
            };
        }

        public async Task<int> GetOrdersCountAsync()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<int> GetInProgressOrdersCountAsync()
        {
            return await _context.Orders.CountAsync(o => o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled);
        }
    }
}
