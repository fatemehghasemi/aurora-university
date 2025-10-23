using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuroraUniversity.Infrastructure.Repositories;

public class MarkRepository(UniversityDbContext context)
    : Repository<Mark>(context), IMarkRepository
{
    public async Task<Mark?> GetByIdAsync(Guid id)
    {
        return await DbContext.Marks.FindAsync(id);
    }

    public async Task<IEnumerable<Mark>> GetMarksByStudentId(Guid studentId)
    {
        return await DbContext.Marks
            .Where(m => m.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mark>> GetMarksByStudentAndAssessment(Guid studentId, Guid assessmentId)
    {
        return await DbContext.Marks
            .Where(m => m.StudentId == studentId && m.AssessmentId == assessmentId)
            .ToListAsync();
    }
}
