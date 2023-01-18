using TireService.Infrastructure;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Core.Domain.Services;

public class SalaryPaymentsToWorkerService : ItemBasicService<SalaryPaymentsToWorker>
{
    private readonly WorkerBalanceService _workerBalanceService;
    
    public SalaryPaymentsToWorkerService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource, WorkerBalanceService workerBalanceService) : base(dbContext, cancellationTokenSource)
    {
        _workerBalanceService = workerBalanceService;
    }

    public override async Task Create(SalaryPaymentsToWorker entity)
    {
        await base.Create(entity);
        await _workerBalanceService.MinusFromBalance(entity.WorkerId, entity.Sum);
    }

    public override async Task Delete(Guid identifier)
    {
        var salaryPayment = await GetById(identifier);
        await Delete(salaryPayment);
        await _workerBalanceService.SumToBalance(salaryPayment.WorkerId, salaryPayment.Sum);
    }
}