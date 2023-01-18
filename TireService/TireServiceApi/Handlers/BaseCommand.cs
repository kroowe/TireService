using MediatR;

namespace TireServiceApi.Handlers;

public abstract class BaseCommand<T> : IRequest<T>, IBaseRequest
{
}
public abstract class BaseCommand : IRequest, IRequest<Unit>, IBaseRequest
{
}