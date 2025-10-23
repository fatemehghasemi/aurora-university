using AuroraUniversity.Application.DTOs;

namespace AuroraUniversity.Application.Interfaces;

public interface ITermModuleService
{
    Task<IEnumerable<TermModuleModel>> GetAllAsync();
    Task<TermModuleModel?> GetByIdAsync(Guid id);
    Task<TermModuleModel> CreateAsync(TermModuleModel dto);
    Task<bool> UpdateAsync(Guid id, TermModuleModel dto);
    Task<bool> DeleteAsync(Guid id);
}
