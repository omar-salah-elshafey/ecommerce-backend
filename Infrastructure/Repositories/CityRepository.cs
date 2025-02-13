using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CityRepository(ApplicationDbContext _context) : ICityRepository
    {
        public async Task<City?> GetByIdAsync(Guid cityId)
        {
            return await _context.Cities
                .Include(c => c.Governorate)
                .FirstOrDefaultAsync(c => c.Id == cityId);
        }

        public async Task<List<City>> GetCitiesByGovernorateIdAsync(Guid governorateId)
        {
            return await _context.Cities
                .Where(c => c.GovernorateId == governorateId)
                .ToListAsync();
        }
    }
}
