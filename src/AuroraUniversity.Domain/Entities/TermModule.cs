namespace AuroraUniversity.Domain.Entities;

public class TermModule : Entity
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int Credits { get; set; }
    public Guid TermId { get; set; }
    public Guid StaffId { get; set; }

    // Navigation
    public Term Term { get; set; } = null!;
    public Staff Staff { get; set; } = null!;
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<Student> EnrolledStudents { get; set; } = [];
    public ICollection<Assessment> Assessments { get; set; } = [];
}
