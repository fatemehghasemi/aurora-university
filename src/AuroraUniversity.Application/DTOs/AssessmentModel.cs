namespace AuroraUniversity.Application.DTOs;

public class AssessmentModel
{
    public Guid Id { get; set; }
    public Guid ModuleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Weight { get; set; }
}
