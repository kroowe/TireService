using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities;

namespace TireService.Core.Domain.Services;

public class ShiftWorkWorkerService
{
    private readonly PostgresContext _dbContext;
    private readonly CancellationToken _token;

    public ShiftWorkWorkerService(
        PostgresContext dbContext,
        CancellationTokenSource cancellationTokenSource)
    {
        _dbContext = dbContext;
        _token = cancellationTokenSource.Token;
    }

    public async Task Create(Guid workerId, Guid shiftWorkId)
    {
        _dbContext.ShiftWorkWorkers.Add(new ShiftWorkWorker
        {
            WorkerId = workerId,
            ShiftWorkId = shiftWorkId
        });
        await _dbContext.SaveChangesAsync(_token);
    }

    public async Task Remove(Guid workerId, Guid shiftWorkId)
    {
        var shiftWorkWorkers = await _dbContext.ShiftWorkWorkers.FirstAsync(x => x.WorkerId == workerId && x.ShiftWorkId == shiftWorkId, _token);
        _dbContext.ShiftWorkWorkers.Remove(shiftWorkWorkers);
        await _dbContext.SaveChangesAsync(_token);
    }
}