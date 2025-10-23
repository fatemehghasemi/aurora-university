namespace AuroraUniversity.Application.DTOs;

public class ModuleGradeModel
{
    public Guid ModuleId { get; set; }
    public string ModuleName { get; set; } = string.Empty;
    public int Credits { get; set; }
    public decimal FinalMark { get; set; }
}
