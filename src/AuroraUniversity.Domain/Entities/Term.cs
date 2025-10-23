namespace AuroraUniversity.Domain.Entities;

public class Term : Entity
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    // Navigation
    public ICollection<TermModule> Modules { get; set; } = [];
}
