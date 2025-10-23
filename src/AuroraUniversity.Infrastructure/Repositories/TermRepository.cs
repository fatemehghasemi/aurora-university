using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuroraUniversity.Infrastructure.Repositories;

public class TermRepository(UniversityDbContext context)
    : Repository<Term>(context), ITermRepository
{
    public async Task<IEnumerable<Term>> GetAllAsync()
    {
        return await DbContext.Terms
            .Include(m => m.Modules)
            .ToListAsync();
    }

    public async Task<Term?> GetByIdAsync(Guid id)
    {
        return await DbContext.Terms
            .Include(m => m.Modules)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Term>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await DbContext.Terms
            .Include(m => m.Modules)
            .Where(m => ids.Contains(m.Id))
            .ToListAsync();
    }
}
