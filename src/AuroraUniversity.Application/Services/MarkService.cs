using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;

namespace AuroraUniversity.Application.Services;

public class MarkService : IMarkService
{
    private readonly IMarkRepository _markRepository;
    public MarkService(IMarkRepository markRepository)
    {
        _markRepository = markRepository;
    }

    public Task<IEnumerable<MarkModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MarkModel>> GetByStudentIdAsync(Guid studentId)
    {
        var marks = await _markRepository.GetMarksByStudentId(studentId);

        return marks.Select(mark => new MarkModel
        {
            Id = mark.Id,
            StudentId = mark.StudentId,
            AssessmentId = mark.AssessmentId,
            Score = mark.Score,
            IsResit = mark.IsResit,
            Comments = mark.Comments
        });
    }

    public async Task<MarkModel> RecordMarkAsync(MarkModel dto)
    {
        if (dto.Score < 0 || dto.Score > 100)
            throw new ArgumentException("Score must be between 0 and 100.");

        var existingMarks = (await _markRepository
            .GetMarksByStudentAndAssessment(dto.StudentId, dto.AssessmentId))
            .ToList();

        if (existingMarks.Count >= 2)
            throw new InvalidOperationException("A student can only have up to 2 marks for a given assessment (initial + resit).");

        var mark = new Mark
        {
            Id = Guid.NewGuid(),
            StudentId = dto.StudentId,
            AssessmentId = dto.AssessmentId,
            Score = dto.Score,
            IsResit = existingMarks.Count == 1,
            RecordedByStaffId = dto.RecordedByStaffId,
            DateRecorded = DateTime.UtcNow,
            Comments = dto.Comments
        };

        var created = await _markRepository.CreateAsync(mark);

        return new MarkModel
        {
            Id = created.Id,
            StudentId = created.StudentId,
            AssessmentId = created.AssessmentId,
            Score = created.Score,
            IsResit = created.IsResit,
            Comments = created.Comments
        };
    }
}
