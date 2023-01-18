using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities;

namespace TireService.Core.Domain.Services;

public class WarehouseItemHistoryService : ItemBasicService<WarehouseItemHistory>
{
    public WarehouseItemHistoryService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource) : base(dbContext, cancellationTokenSource)
    {
    }

    public async Task<IReadOnlyCollection<WarehouseItemHistory>> GetByWarehouseNomenclature(
        Guid warehouseNomenclatureId)
    {
        return await _dbSet.Where(x => x.WarehouseNomenclatureId == warehouseNomenclatureId).ToListAsync(_token);
    }
}