using AuroraUniversity.Application.DTOs;

namespace AuroraUniversity.Application.Interfaces;

public interface ITermService
{
    Task<IEnumerable<TermModel>> GetAllAsync();
    Task<TermModel?> GetByIdAsync(Guid id);
    Task<TermModel> CreateAsync(TermModel dto);
    Task<bool> UpdateAsync(Guid id, TermModel dto);
    Task<bool> DeleteAsync(Guid id);
}
