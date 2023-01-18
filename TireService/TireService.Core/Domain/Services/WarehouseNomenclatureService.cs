using Microsoft.EntityFrameworkCore;
using TireService.Core.Utils;
using TireService.Dtos.Infos.Warehouse;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities;

namespace TireService.Core.Domain.Services;

public class WarehouseNomenclatureService : ItemBasicService<WarehouseNomenclature>
{
    public WarehouseNomenclatureService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource) : base(dbContext, cancellationTokenSource)
    {
    }

    public async Task<IReadOnlyCollection<WarehouseNomenclature>> GetAllWarehouseNomenclatures(GetAllWarehouseNomenclatureInfo info, string include = null)
    {
        var query = _dbSet.AsQueryable();
        
        if (string.IsNullOrEmpty(info.Name) == false)
        {
            query = query.Where(x => x.Name.Contains(info.Name));
        }
        
        if (string.IsNullOrEmpty(info.Article) == false)
        {
            query = query.Where(x => x.Article.Contains(info.Name));
        }

        if (info.WarehouseNomenclatureIds != null && info.WarehouseNomenclatureIds.Any())
        {
            query = query.Where(x => info.WarehouseNomenclatureIds.Contains(x.Id));
        }

        EntityFrameworkHelper.SetEfIncludeString(query, include);

        return await query.ToListAsync(_token);
    }
}