using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities;

namespace TireService.Core.Domain.Services;

public class TaskOrderService : ItemBasicService<TaskOrder>
{
    public TaskOrderService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource) : base(dbContext, cancellationTokenSource)
    {
    }

    public async Task<IReadOnlyCollection<TaskOrder>> GetTaskOrderByOrder(Guid orderId)
    {
        return await _dbSet.Where(x => x.OrderId == orderId).ToListAsync(_token);
    }
}