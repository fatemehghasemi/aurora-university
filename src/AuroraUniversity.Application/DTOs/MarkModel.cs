namespace AuroraUniversity.Application.DTOs;

public class MarkModel
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid AssessmentId { get; set; }
    public decimal Score { get; set; }
    public bool IsResit { get; set; }
    public string? Comments { get; set; }
    public Guid RecordedByStaffId { get; set; }
}
