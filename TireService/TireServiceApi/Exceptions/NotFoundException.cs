using System.Net;

namespace TireServiceApi.Exceptions;

public class NotFoundException : BaseException
{

    public NotFoundException() : base("not found")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
    public override HttpStatusCode GetHttpCode()
    {
        return HttpStatusCode.NotFound;
    }
}
