using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Core.Domain.Services;

public class WorkerBalanceService : ItemBasicService<WorkerBalance>
{
    public WorkerBalanceService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource) : base(dbContext, cancellationTokenSource)
    {
    }

    public async Task SumToBalance(Guid workerId, decimal sum)
    {
        var balance = await _dbSet.FirstAsync(x => x.WorkerId == workerId, _token);
        balance.Balance += sum;
        await Update(balance);
    }

    public async Task MinusFromBalance(Guid workerId, decimal sum)
    {
        var balance = await _dbSet.FirstAsync(x => x.WorkerId == workerId, _token);
        balance.Balance -= sum;
        await Update(balance);
    }
}