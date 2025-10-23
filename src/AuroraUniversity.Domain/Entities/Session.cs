namespace AuroraUniversity.Domain.Entities;

public class Session : Entity
{
    public Guid ModuleId { get; set; }
    public Guid StaffId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; } = string.Empty;

    // Navigation
    public TermModule Module { get; set; } = null!;
    public Staff Staff { get; set; } = null!;
    public ICollection<Student> RegisteredStudents { get; set; } = [];
}