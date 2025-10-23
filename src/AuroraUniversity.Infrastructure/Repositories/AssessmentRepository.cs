
using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuroraUniversity.Infrastructure.Repositories;

public class AssessmentRepository(UniversityDbContext context)
    : Repository<Assessment>(context), IAssessmentRepository
{
    public async Task<IEnumerable<Assessment>> GetAllAsync()
    {
        return await DbContext.Assessments.ToListAsync();
    }

    public async Task<Assessment?> GetByIdAsync(Guid id)
    {
        return await DbContext.Assessments.FindAsync(id);
    }
}
