namespace AuroraUniversity.Domain.Entities;

public class Assessment : Entity
{
    public Guid ModuleId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Weight { get; set; }

    // Navigation
    public TermModule Module { get; set; } = null!;
    public ICollection<Mark> Marks { get; set; } = [];
}
