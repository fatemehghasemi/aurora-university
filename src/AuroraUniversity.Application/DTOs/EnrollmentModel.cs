namespace AuroraUniversity.Application.DTOs;

public class EnrollmentModel
{
    public IEnumerable<Guid> StudentIds { get; set; }
    public Guid ModuleId { get; set; }
}
