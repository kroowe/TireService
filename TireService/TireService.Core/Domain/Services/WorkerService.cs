using Microsoft.EntityFrameworkCore;
using TireService.Dtos.Infos.Worker;
using TireService.Dtos.Views.Worker;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Core.Domain.Services;

public class WorkerService : ItemBasicService<Worker>
{
    private readonly WorkerBalanceService _balanceService;
    public WorkerService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource, WorkerBalanceService balanceService) : base(dbContext, cancellationTokenSource)
    {
        _balanceService = balanceService;
    }

    public override async Task Create(Worker entity)
    {
        await base.Create(entity);
        await _balanceService.Create(new WorkerBalance
        {
            WorkerId = entity.Id,
            Balance = 0
        });
    }

    public async Task<IReadOnlyCollection<Worker>> GetAllByFilter(GetAllWorkerByFilterInfo info)
    {
        var query = _dbSet.AsQueryable();

        if (string.IsNullOrEmpty(info.Name) == false)
        {
            query = query.Where(x => x.Name.Contains(info.Name));
        }

        if (string.IsNullOrEmpty(info.Surname) == false)
        {
            query = query.Where(x => x.Surname.Contains(info.Surname));
        }

        if (string.IsNullOrEmpty(info.Patronymic) == false)
        {
            query = query.Where(x => x.Patronymic.Contains(info.Patronymic));
        }

        if (string.IsNullOrEmpty(info.PhoneNumber) == false)
        {
            query = query.Where(x => x.PhoneNumber == info.PhoneNumber);
        }

        if (info.WorkerIds != null && info.WorkerIds.Any())
        {
            query = query.Where(x => x.Name == info.Name);
        }

        if (info.ExcludeWorkerIds != null && info.ExcludeWorkerIds.Any())
        {
            query = query.Where(x => info.ExcludeWorkerIds.Contains(x.Id) == false);
        }

        if (info.IsDismissed.HasValue == true)
        {
            query = query.Where(x => x.IsDismissed == info.IsDismissed);
        }

        return await query.ToArrayAsync(_token);
    }

    public async Task SetDismissed(Guid workerId, bool isDismissed)
    {
        var worker = await _dbSet.FirstAsync(x => x.Id == workerId);
        worker.IsDismissed = isDismissed;
        await Update(worker);
    }

    public async Task<IReadOnlyCollection<Worker>> GetAllWorkerWithBalanceAndSalaryPayment(
        GetAllWorkerWithBalanceAndSalaryPaymentInfo info)
    {
        var query = _dbSet
            .Include(x => x.WorkerBalance)
            .Include(x => x.SalaryPaymentsToWorkers)
            .AsQueryable();
        
        if (info.WithDismissedWorkers == false)
        {
           query = query.Where(x => x.IsDismissed == false);
        }

        return await query.ToListAsync(_token);
    }
}