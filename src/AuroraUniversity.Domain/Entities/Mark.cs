namespace AuroraUniversity.Domain.Entities;

public class Mark : Entity
{
    public Guid StudentId { get; set; }
    public Guid AssessmentId { get; set; }
    public decimal Score { get; set; }
    public bool IsResit { get; set; }
    public DateTime DateRecorded { get; set; } = DateTime.UtcNow;
    public string? Comments { get; set; }
    public Guid? RecordedByStaffId { get; set; }

    // Navigation
    public Student Student { get; set; } = null!;
    public Assessment Assessment { get; set; } = null!;
    public Staff? RecordedByStaff { get; set; }
}
