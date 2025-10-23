using AuroraUniversity.Application.DTOs;
using AuroraUniversity.Application.Interfaces;
using AuroraUniversity.Domain.Entities;
using AuroraUniversity.Domain.Interfaces;

namespace AuroraUniversity.Application.Services;

public class TermModuleService(ITermModuleRepository repository) : ITermModuleService
{
    private readonly ITermModuleRepository _repository = repository;

    public async Task<IEnumerable<TermModuleModel>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(MapToModel);
    }

    public async Task<TermModuleModel?> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : MapToModel(entity);
    }

    public async Task<TermModuleModel> CreateAsync(TermModuleModel dto)
    {
        var entity = MapToEntity(dto);
        entity = await _repository.CreateAsync(entity);
        return MapToModel(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, TermModuleModel dto)
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

    private static TermModuleModel MapToModel(TermModule entity)
    {
        return new TermModuleModel
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Title,
            Capacity = entity.Capacity,
            StaffId = entity.StaffId,
            EnrolledCount = entity.EnrolledStudents?.Count ?? 0,
        };
    }

    private static TermModule MapToEntity(TermModuleModel model)
    {
        return new TermModule
        {
            Id = model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
            Code = model.Code,
            Title = model.Name,
            Capacity = model.Capacity,
            StaffId = model.StaffId
        };
    }
}
