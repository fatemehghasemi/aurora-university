
using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;

namespace AuroraUniversity.Application.Services;

public class AssessmentService : IAssessmentService
{
    private readonly IAssessmentRepository _assessmentRepository;
    public AssessmentService(IAssessmentRepository assessmentRepository)
    {
        _assessmentRepository = assessmentRepository;
    }

    public async Task<AssessmentModel> CreateAssessmentAsync(AssessmentModel dto)
    {
        var assessment = new Assessment
        {
            Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            ModuleId = dto.ModuleId,
            Title = dto.Name,
            Date = dto.Date,
            Weight = dto.Weight
        };

        var created = await _assessmentRepository.CreateAsync(assessment);
        dto.Id = created.Id;
        return dto;
    }

    public async Task<IEnumerable<AssessmentModel>> GetAllAsync()
    {
        var assessments = await _assessmentRepository.GetAllAsync();
        return assessments.Select(a => new AssessmentModel
        {
            Id = a.Id,
            ModuleId = a.ModuleId,
            Name = a.Title,
            Date = a.Date,
            Weight = a.Weight
        });
    }

    public async Task<AssessmentModel?> GetByIdAsync(Guid id)
    {
        var a = await _assessmentRepository.GetByIdAsync(id);
        if (a == null) return null;
        return new AssessmentModel
        {
            Id = a.Id,
            ModuleId = a.ModuleId,
            Name = a.Title,
            Date = a.Date,
            Weight = a.Weight
        };
    }
}
