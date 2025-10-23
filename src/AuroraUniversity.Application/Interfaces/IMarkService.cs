
using AuroraUniversity.Application.DTOs;

namespace AuroraUniversity.Application.Interfaces;

public interface IMarkService
{
    Task<IEnumerable<MarkModel>> GetAllAsync();
    Task<IEnumerable<MarkModel>> GetByStudentIdAsync(Guid studentId);
    Task<MarkModel> RecordMarkAsync(MarkModel mark);
}
