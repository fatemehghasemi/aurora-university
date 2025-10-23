using AuroraUniversity.Domain.Entities;

namespace AuroraUniversity.Domain.Interfaces;

public interface IMarkRepository : IRepository<Mark>
{
    Task<Mark?> GetByIdAsync(Guid id);
    Task<IEnumerable<Mark>> GetMarksByStudentId(Guid studentId);
    Task<IEnumerable<Mark>> GetMarksByStudentAndAssessment(Guid studentId, Guid assessmentId);
}
