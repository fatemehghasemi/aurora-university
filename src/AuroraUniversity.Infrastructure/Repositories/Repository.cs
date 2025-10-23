using AuroraUniversity.Domain.Interfaces;

namespace AuroraUniversity.Infrastructure.Repositories;

public class Repository<TEntity>(UniversityDbContext dbContext)
    : IRepository<TEntity> where TEntity : class, new()
{
    protected UniversityDbContext DbContext { get; } = dbContext;

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await DbContext.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbContext.Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await DbContext.FindAsync<TEntity>(id);
        if (entity is null)
            return false;

        DbContext.Remove(entity);
        var affectedRows = await DbContext.SaveChangesAsync();
        return affectedRows > 0;
    }
}
