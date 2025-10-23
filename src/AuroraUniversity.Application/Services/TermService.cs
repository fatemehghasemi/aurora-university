using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;

namespace AuroraUniversity.Application.Services;

public class TermService(ITermRepository repository) : ITermService
{
    private readonly ITermRepository _repository = repository;

    public async Task<IEnumerable<TermModel>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(MapToModel);
    }

    public async Task<TermModel?> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : MapToModel(entity);
    }

    public async Task<TermModel> CreateAsync(TermModel dto)
    {
        var entity = MapToEntity(dto);
        entity = await _repository.CreateAsync(entity);
        return MapToModel(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, TermModel dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return false;

        entity.Code = dto.Code;
        entity.Title = dto.Name;
        entity.Capacity = dto.Capacity;
        entity.StaffId = dto.StaffId;

        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static TermModel MapToModel(Term entity)
    {
        return new TermModel
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Title,
            Capacity = entity.Capacity,
            StaffId = entity.StaffId,
            EnrolledCount = entity.EnrolledStudents?.Count ?? 0,
        };
    }

    private static Term MapToEntity(TermModel model)
    {
        return new Term
        {
            Id = model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
            Code = model.Code,
            Title = model.Name,
            Capacity = model.Capacity,
            StaffId = model.StaffId
        };
    }
}
