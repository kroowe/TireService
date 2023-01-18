namespace TireServiceApi.Infrastructure;

public interface ITransactionMediator
{
    Task<ITransaction> BeginTransaction();
}
