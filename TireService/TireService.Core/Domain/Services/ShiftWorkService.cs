using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities;

namespace TireService.Core.Domain.Services;

public class ShiftWorkService : ItemBasicService<ShiftWork>
{
    private readonly ShiftWorkWorkerService _shiftWorkWorkerService;
    private readonly PaymentRuleService _paymentRuleService;
    private readonly WorkerBalanceService _workerBalanceService;
    
    public ShiftWorkService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource, ShiftWorkWorkerService shiftWorkWorkerService, PaymentRuleService paymentRuleService, WorkerBalanceService workerBalanceService) : base(dbContext, cancellationTokenSource)
    {
        _shiftWorkWorkerService = shiftWorkWorkerService;
        _paymentRuleService = paymentRuleService;
        _workerBalanceService = workerBalanceService;
    }

    public async Task OpenShiftWork(Guid shiftWorkId)
    {
        var anotherOpen = await _dbSet.FirstOrDefaultAsync(x => x.IsOpen == true, _token);

        if (anotherOpen != null)
        {
            throw new Exception($"Shift {anotherOpen.Id} by {anotherOpen.OpenedDate} is already open");
        }

        var shiftWork = await _dbSet.FirstAsync(x => x.Id == shiftWorkId, _token);
        shiftWork.OpenedDate = DateTime.Now;
        shiftWork.IsOpen = true;
        await Update(shiftWork);
    }

    public async Task CloseShiftWork(Guid shiftWorkId)
    {
        var shiftWork = await _dbSet
            .Include(x => x.ShiftWorkWorkers)
            .FirstAsync(x => x.Id == shiftWorkId, _token);

        if (shiftWork.IsOpen == false)
        {
            throw new Exception($"Shift {shiftWork.Id} by {shiftWork.OpenedDate} is already close");
        }

        CloseShiftWork(shiftWork);
        await Update(shiftWork);

        foreach (var shiftWorkWorker in shiftWork.ShiftWorkWorkers)
        {
            var sumByWorker = await _paymentRuleService.GetSumByCloseShiftWork(shiftWorkWorker.WorkerId);
            await _workerBalanceService.SumToBalance(shiftWorkWorker.WorkerId, sumByWorker);
        }
    }

    public async Task AddWorkerToShiftWork(Guid workerId, Guid shiftWorkId)
    {
        await _shiftWorkWorkerService.Create(workerId, shiftWorkId);
    }

    public async Task RemoveWorkerToShiftWork(Guid workerId, Guid shiftWorkId)
    {
        await _shiftWorkWorkerService.Remove(workerId, shiftWorkId);
    }

    public async Task<IReadOnlyCollection<ShiftWork>> GetShiftWorksByStartDate(int year, int month, int day)
    {
        var startDateMin = new DateTime(year, month, day);
        var startDateMax = startDateMin.AddDays(1);
        var shiftWorks = await _dbSet
            .Where(x => x.CreatedDate > startDateMin && x.CreatedDate < startDateMax)
            .Include(x => x.Workers)
            .ToListAsync(_token);
        return shiftWorks;
    }

    public async Task<ShiftWork> GetOpenShiftWork()
    {
        var openShiftWork = await _dbSet
            .Include(x => x.ShiftWorkWorkers)
            .ThenInclude(x => x.Worker)
            .FirstOrDefaultAsync(x => x.IsOpen, _token);
        return openShiftWork;
    }

    private ShiftWork CloseShiftWork(ShiftWork shiftWork)
    {
        shiftWork.IsOpen = false;
        shiftWork.ClosedDate = DateTime.Now;
        return shiftWork;
    }
}