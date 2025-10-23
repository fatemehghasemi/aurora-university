using AuroraUniversity.Domain.Entities;

namespace AuroraUniversity.Domain.Interfaces;

public interface ITermRepository : IRepository<Term>
{
    Task<Term?> GetByIdAsync(Guid id);
    Task<IEnumerable<Term>> GetAllAsync();
    Task<IEnumerable<Term>> GetByIdsAsync(IEnumerable<Guid> ids);
}