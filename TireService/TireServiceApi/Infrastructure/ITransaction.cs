namespace TireServiceApi.Infrastructure;

public interface ITransaction : IDisposable
{
    void Commit();

    void Rollback();
}
