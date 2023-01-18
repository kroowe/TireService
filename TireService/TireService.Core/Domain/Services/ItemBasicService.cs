using Microsoft.EntityFrameworkCore;
using TireService.Core.Utils;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Core.Domain.Services;

public abstract class ItemBasicService<TItem> where TItem : BaseEntity
{
    protected readonly DbSet<TItem> _dbSet;
    private readonly PostgresContext _dbContext;
    protected readonly CancellationToken _token;

    public ItemBasicService(
        PostgresContext dbContext,
        CancellationTokenSource cancellationTokenSource)
    {
        _dbSet = dbContext.Set<TItem>();
        _dbContext = dbContext;
        _token = cancellationTokenSource.Token;
    }

    public virtual async Task Create(TItem entity)
    {
        _dbSet.Add(entity);
        await _dbContext.SaveChangesAsync(_token);
        _dbContext.Entry(entity).State = EntityState.Detached;
    }

    public async Task CreateAll(IReadOnlyCollection<TItem> entities)
    {
        var entries = _dbContext.ChangeTracker.Entries();
        _dbSet.AddRange(entities);
        await _dbContext.SaveChangesAsync(_token);
        var toDetached = _dbContext.ChangeTracker
            .Entries()
            .Where(e => !entries.Contains(e));

        //TODO: Подумать, может быть стоит вообще выключить AutoDetectChangesEnabled
        _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        foreach (var entry in toDetached)
            entry.State = EntityState.Detached;
        _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
    }
    
    public virtual async Task Update(TItem entity)
    {
        var entries = _dbContext.ChangeTracker.Entries();
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync(_token);
        var toDetached = _dbContext.ChangeTracker.Entries().Where(e => !entries.Contains(e));
        _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        foreach (var entry in toDetached)
            entry.State = EntityState.Detached;
        _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
    }

    public virtual async Task UpdateAll(IReadOnlyCollection<TItem> entities)
    {
        var entries = _dbContext.ChangeTracker.Entries();
        _dbSet.UpdateRange(entities);
        await _dbContext.SaveChangesAsync(_token);

        var toDetached = _dbContext.ChangeTracker.Entries().Where(e => !entries.Contains(e));
        _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        foreach (var entry in toDetached)
            entry.State = EntityState.Detached;
        _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
    }
    
    public virtual async Task Delete(Guid identifier)
    {
        var entity = await _dbSet
            .AsNoTracking()
            .Where(a => a.Id == identifier)
            .FirstOrDefaultAsync(_token);

        await Delete(entity);
    }
    
    public virtual async Task Delete(TItem entity)
    {
        if (entity != null)
        {
            if (entity is IHaveIsDeleted entityIsDeleted)
            {
                entityIsDeleted.IsDeleted = true;
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        await _dbContext.SaveChangesAsync(_token);
    }

    public async Task DeleteAll(IReadOnlyCollection<TItem> entities)
    {
        var first = entities.First();
        
        if (first is IHaveIsDeleted)
        {
            foreach (var item in entities)
            {
                var entity = (IHaveIsDeleted) item;
                entity.IsDeleted = true;
            }
            _dbSet.UpdateRange(entities);
        }
        else
        {
            _dbSet.RemoveRange(entities);
        }
        
        await _dbContext.SaveChangesAsync(_token);
    }

    public async Task<TItem> GetById(Guid id, string include = null)
    {
        var query = (IQueryable<TItem>)_dbSet;
        query = EntityFrameworkHelper.SetEfIncludeString(query, include);
        return await query.FirstAsync(x => x.Id == id, _token);
    }

    public async Task<IReadOnlyCollection<TItem>> GetAll()
    {
        return await _dbSet.ToListAsync(_token);
    }
}