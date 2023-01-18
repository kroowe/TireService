using Microsoft.AspNetCore.Builder;
using TireServiceApi.Infrastructure.Middleware;

namespace TireServiceApi.Extensions;

public static class GlobalExceptionsMiddlewareApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalExceptionsMiddleware(this IApplicationBuilder app)
    {
        if (app == null)
            throw new ArgumentNullException(nameof(app));

        return app.UseMiddleware<GlobalExceptionsMiddleware>();
    }
}
