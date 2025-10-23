using AuroraUniversity.Domain.Entities;

namespace AuroraUniversity.Application.DTOs;

public class TermModuleModel
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int Credits { get; set; }
    public Guid StaffId { get; set; }
    public string? StaffName { get; set; }
    public int EnrolledCount { get; set; }
    public int SeminarGroupCount { get; set; }


    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
}
