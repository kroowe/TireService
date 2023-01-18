using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure;

namespace TireServiceApi.Infrastructure;

public class TransactionMediator : ITransactionMediator
{
    private readonly PostgresContext _postgresContext;
    private readonly CancellationToken _cancellationToken;

    public TransactionMediator(
        PostgresContext postgresContext,
        CancellationTokenSource cancellationTokenSource)
    {
        _postgresContext = postgresContext;
        _postgresContext.Database.SetCommandTimeout(TimeSpan.FromHours(2));
        _cancellationToken = cancellationTokenSource.Token;
    }

    public async Task<ITransaction> BeginTransaction()
    {
        return new Transaction(await _postgresContext.Database.BeginTransactionAsync(_cancellationToken));
    }
}
