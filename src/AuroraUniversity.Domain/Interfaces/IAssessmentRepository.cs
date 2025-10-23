
using AuroraUniversity.Domain.Entities;

namespace AuroraUniversity.Domain.Interfaces;

public interface IAssessmentRepository : IRepository<Assessment>
{
    Task<IEnumerable<Assessment>> GetAllAsync();
    Task<Assessment?> GetByIdAsync(Guid id);
}
