namespace AuroraUniversity.Application.DTOs;

public class SeminarGroupModel
{
    public Guid Id { get; set; }
    public Guid ModuleId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
