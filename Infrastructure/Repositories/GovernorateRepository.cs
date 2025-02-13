using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GovernorateRepository(ApplicationDbContext _context) : IGovernorateRepository
    {
        public async Task<Governorate?> GetByIdAsync(Guid governorateId)
        {
            return await _context.Governorates
                .Include(g => g.Cities)
                .FirstOrDefaultAsync(g => g.Id == governorateId);
        }

        public async Task<List<Governorate>> GetAllAsync()
        {
            return await _context.Governorates
                .Include(g => g.Cities)
                .ToListAsync();
        }
    }
}
