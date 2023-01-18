using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TireServiceApi.Exceptions;
using TireServiceApi.View;

namespace TireServiceApi.Infrastructure.Middleware;

public sealed class GlobalExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IWebHostEnvironment environment)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            var statusCode = exception switch
            {
                BaseException treesException => treesException.GetHttpCode(),
                NotImplementedException => HttpStatusCode.NotImplemented,
                _ => HttpStatusCode.InternalServerError
            };

            var response = httpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";

            var exceptionType = exception.GetType();
            var result = JsonConvert.SerializeObject(new ErrorView
            {
                Type = exceptionType?.Name,
                Message = exception.Message,
                StackTrace = environment.IsDevelopment()
                    ? exception.StackTrace?.Split("\r\n")
                    : Array.Empty<string>()
            });

            await response.WriteAsync(result);
        }
    }
}
