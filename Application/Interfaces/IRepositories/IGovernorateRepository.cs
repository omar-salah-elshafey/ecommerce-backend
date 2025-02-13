using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IGovernorateRepository
    {
        Task<Governorate?> GetByIdAsync(Guid governorateId);
        Task<List<Governorate>> GetAllAsync();
    }

}
