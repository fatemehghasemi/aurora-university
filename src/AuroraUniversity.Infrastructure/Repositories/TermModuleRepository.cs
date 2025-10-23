using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuroraUniversity.Infrastructure.Repositories;

public class TermModuleRepository(UniversityDbContext context)
    : Repository<TermModule>(context), ITermModuleRepository
{
    public async Task<IEnumerable<TermModule>> GetAllAsync()
    {
        return await DbContext.Modules
            .Include(m => m.Sessions)
            .Include(m => m.EnrolledStudents)
            .ToListAsync();
    }

    public async Task<TermModule?> GetByIdAsync(Guid id)
    {
        return await DbContext.Modules
            .Include(m => m.Sessions)
            .Include(m => m.EnrolledStudents)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<TermModule>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await DbContext.Modules
            .Include(m => m.Sessions)
            .Include(m => m.EnrolledStudents)
            .Where(m => ids.Contains(m.Id))
            .ToListAsync();
    }
}
