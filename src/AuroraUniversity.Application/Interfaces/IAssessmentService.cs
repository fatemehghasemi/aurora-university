
using AuroraUniversity.Application.DTOs;

namespace AuroraUniversity.Application.Interfaces;

public interface IAssessmentService
{
    Task<IEnumerable<AssessmentModel>> GetAllAsync();
    Task<AssessmentModel?> GetByIdAsync(Guid id);
    Task<AssessmentModel> CreateAssessmentAsync(AssessmentModel assessment);
}
