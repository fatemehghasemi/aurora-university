using AuroraUniversity.Domain.Entities;

namespace AuroraUniversity.Domain.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task<Student?> GetByIdAsync(Guid id);
    Task<IEnumerable<Student>> GetForEnrollmentCheckAsync(IEnumerable<Guid> studentIds);
    bool HasTimeConflict(Student student, TermModule module);

}
