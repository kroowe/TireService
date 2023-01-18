using Microsoft.EntityFrameworkCore.Storage;

namespace TireServiceApi.Infrastructure;

public class Transaction : ITransaction
{
    private readonly IDbContextTransaction _dbContextTransaction;

    public Transaction(IDbContextTransaction dbContextTransaction)
    {
        _dbContextTransaction = dbContextTransaction;
    }

    public void Commit()
    {
        _dbContextTransaction.Commit();
    }

    public void Dispose()
    {
        _dbContextTransaction.Dispose();
    }

    public void Rollback()
    {
        _dbContextTransaction.Rollback();
    }
}
