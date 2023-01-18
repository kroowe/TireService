using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Core.Domain.Services;

public class PaymentRuleService : ItemBasicService<PaymentRule>
{
    private readonly WorkerBalanceService _workerBalanceService;
    
    public PaymentRuleService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource, WorkerBalanceService workerBalanceService) : base(dbContext, cancellationTokenSource)
    {
        _workerBalanceService = workerBalanceService;
    }

    public async Task<IReadOnlyCollection<PaymentRule>> GetByWorker(Guid workerId)
    {
        return await _dbSet
            .Where(x => x.WorkerId == workerId)
            .ToListAsync(_token);

    }

    public async Task<decimal> GetSumFromOrder(Guid workerId, decimal taskOrderSum)
    {
        var workerRules = await GetByWorker(workerId);
        var procentRules = workerRules.Where(x => x.SalaryType == SalaryType.PaymentPieceWork).ToArray();
        var sumFromOrder = procentRules.Select(x => (x.SumBySalaryType / 100) * taskOrderSum).Sum();
        return sumFromOrder;
    }

    public async Task<decimal> GetSumByCloseShiftWork(Guid workerId)
    {
        var workerRules = await GetByWorker(workerId);
        var procentRules = workerRules.Where(x => x.SalaryType == SalaryType.PaymentPerDay).ToArray();
        return procentRules.Sum(x => x.SumBySalaryType);
    }
}