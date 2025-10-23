namespace AuroraUniversity.Domain.Entities;

public class Staff : Entity
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    // Navigation
    public ICollection<TermModule> ManagedModules { get; set; } = [];
    public ICollection<Session> TaughtSessions { get; set; } = [];
}
