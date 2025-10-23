namespace AuroraUniversity.Application.DTOs;

public class StudentGradesModel
{
    public Guid StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<ModuleGradeModel> ModuleGrades { get; set; } = new();

    public decimal TermAverage { get; set; }
    public DateTime AssessmentStart { get; set; }
    public DateTime AssessmentEnd { get; set; }
    public string ProgressionDecision { get; set; } = string.Empty;
}
