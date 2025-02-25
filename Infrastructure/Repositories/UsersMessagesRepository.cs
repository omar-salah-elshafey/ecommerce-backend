using Application.Interfaces.IRepositories;
using Application.Models;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UsersMessagesRepository(ApplicationDbContext _context) : IUsersMessagesRepository
    {
        public async Task CreateMessageAsync(UsersMessage usersMessage)
        {
            await _context.UsersMessages.AddAsync(usersMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<UsersMessage?> GetMessageAsync(Guid id)
        {
            return await _context.UsersMessages.FindAsync(id);
        }

        public async Task<PaginatedResponseModel<UsersMessage>> GetMessagesAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.UsersMessages.CountAsync();
            var messages = await _context.UsersMessages
                .OrderByDescending(m => m.MessageDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResponseModel<UsersMessage>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = messages
            };
        }

        public async Task UpdateMessageStatusAsync(UsersMessage usersMessage)
        {
            _context.UsersMessages.Update(usersMessage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(UsersMessage usersMessage)
        {
            _context.UsersMessages.Remove(usersMessage);
            await _context.SaveChangesAsync();
        }
    }
}
