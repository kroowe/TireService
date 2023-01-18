using MediatR;
using TireServiceApi.Infrastructure;

namespace TireServiceApi.Handlers;

public class PipeLineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ITransactionMediator _transactionMediator;

    public PipeLineBehavior(ITransactionMediator transactionMediator)
    {
        _transactionMediator = transactionMediator;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        TResponse response;

        if ((request is BaseCommand<TResponse> reqGenericCommand || request is BaseCommand reqCommand))
        {
            var transaction = await _transactionMediator.BeginTransaction();
            try
            {
                response = await next();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

            return response;
        }

        response = await next();
        return response;
    }
}
