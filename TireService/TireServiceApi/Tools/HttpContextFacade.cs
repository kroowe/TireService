using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace TireServiceApi.Tools;

public class HttpContextFacade
{
    private readonly HttpResponse _httpResponse;

    public HttpContextFacade(IHttpContextAccessor httpContextAccessor)
    {
        _httpResponse = httpContextAccessor?.HttpContext?.Response;
    }

    public void AddToResponseHeader(string key, StringValues value)
    {
        if (_httpResponse == null) return;

        if (_httpResponse.Headers.ContainsKey(key))
        {
            _httpResponse.Headers[key] = value;
        }
        else
        {
            _httpResponse.Headers.Add(new KeyValuePair<string, StringValues>(key, value));
        }
    }
}
