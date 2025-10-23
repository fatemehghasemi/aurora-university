using AuroraUniversity.Application.DTOs;

namespace AuroraUniversity.Application.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentModel>> GetAllAsync();
    Task<StudentModel?> GetByIdAsync(Guid id);
    Task<StudentModel> CreateAsync(StudentModel student);
    Task<bool> UpdateAsync(StudentModel student);
    Task<bool> DeleteAsync(Guid id);
}
