using AuroraUniversity.Domain.Entities;

namespace AuroraUniversity.Domain.Interfaces;

public interface ITermModuleRepository : IRepository<TermModule>
{
    Task<TermModule?> GetByIdAsync(Guid id);
    Task<IEnumerable<TermModule>> GetAllAsync();
    Task<IEnumerable<TermModule>> GetByIdsAsync(IEnumerable<Guid> ids);
}