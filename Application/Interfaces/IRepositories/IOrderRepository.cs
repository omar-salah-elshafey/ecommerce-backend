using Application.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.IRepositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateOrderStatusAsync(Order order);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task<List<Order>> GetOrdersByUserNameAsync(string userName);
        Task<List<Order>> GetInProgressOrdersByUserNameAsync(string userName);
        Task<PaginatedResponseModel<Order>> GetAllOrdersAsync(int pageNumber, int pageSize);
        Task<PaginatedResponseModel<Order>> GetAllInProgressOrdersAsync(int pageNumber, int pageSize);
        Task<Address?> GetAddressByIdAsync(Guid addressId);
        Task<List<Address>> GetUserAddressesAsync(string userId);
        Task<int> GetOrdersCountAsync();
        Task<int> GetInProgressOrdersCountAsync();
    }
}
