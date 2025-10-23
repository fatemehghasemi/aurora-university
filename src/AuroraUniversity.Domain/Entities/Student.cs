namespace AuroraUniversity.Domain.Entities;

public class Student : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    // Navigation
    public ICollection<TermModule> EnrolledModules { get; set; } = [];
    public ICollection<Session> RegisteredSessions { get; set; } = [];
    public ICollection<Mark> Marks { get; set; } = [];
}
