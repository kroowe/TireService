using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Core.Domain.Services;

public class CatalogItemService : ItemBasicService<CatalogItem>
{
    public CatalogItemService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource) : base(dbContext, cancellationTokenSource)
    {
    }

    public async Task<IReadOnlyCollection<CatalogItem>> GetByCategory(Guid categoryId)
    {
        return await _dbSet.Where(x => x.CategoryId == categoryId && x.IsDeleted == false).ToListAsync(_token);
    }

    public override async Task Update(CatalogItem entity)
    {
        await base.Update(entity);
    }
}