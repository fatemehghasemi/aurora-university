using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;

namespace AuroraUniversity.Application.Services;

public class StudentService(IStudentRepository repository) : IStudentService
{
    private readonly IStudentRepository _repository = repository;

    public async Task<IEnumerable<StudentModel>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(s => new StudentModel
        {
            Id = s.Id,
            LastName = s.LastName,
            FirstName = s.FirstName
        });
    }

    public async Task<StudentModel?> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return null;

        return new StudentModel
        {
            Id = entity.Id,
            LastName = entity.LastName,
            FirstName = entity.FirstName
        };
    }

    public async Task<StudentModel> CreateAsync(StudentModel studentDto)
    {
        var student = new Student
        {
            LastName = studentDto.LastName,
            FirstName = studentDto.FirstName
        };

        var entity = await _repository.CreateAsync(student);
        return new StudentModel
        {
            Id = entity.Id,
            LastName = studentDto.LastName,
            FirstName = studentDto.FirstName
        };
    }

    public async Task<bool> UpdateAsync(StudentModel studentDto)
    {
        var entity = await _repository.GetByIdAsync(studentDto.Id);
        if (entity is null) return false;

        entity.FirstName = studentDto.FirstName;
        entity.LastName = studentDto.LastName;

        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }
}
