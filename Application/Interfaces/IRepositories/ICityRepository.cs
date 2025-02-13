using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ICityRepository
    {
        Task<City?> GetByIdAsync(Guid cityId);
        Task<List<City>> GetCitiesByGovernorateIdAsync(Guid governorateId);
    }
}
