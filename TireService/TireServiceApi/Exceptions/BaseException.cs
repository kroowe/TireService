using System.Net;

namespace TireServiceApi.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(string message) : base(message)
    {
    }

    protected BaseException()
    {
    }

    public abstract HttpStatusCode GetHttpCode();
}
