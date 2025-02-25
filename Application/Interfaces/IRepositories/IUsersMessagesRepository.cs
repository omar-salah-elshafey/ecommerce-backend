using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IUsersMessagesRepository
    {
        Task CreateMessageAsync(UsersMessage usersMessage);
        Task<PaginatedResponseModel<UsersMessage>> GetMessagesAsync(int pageNumber, int pageSize);
        Task<UsersMessage?> GetMessageAsync(Guid id);
        Task UpdateMessageStatusAsync(UsersMessage usersMessage);
        Task DeleteMessageAsync(UsersMessage usersMessage);
    }
}
