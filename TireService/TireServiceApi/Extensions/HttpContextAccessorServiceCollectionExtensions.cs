using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TireServiceApi.Extensions
{
    public static class HttpContextAccessorServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpContextAccessor(this IServiceCollection services)
        {
            HttpServiceCollectionExtensions.AddHttpContextAccessor(services);
            services.AddTransient(a =>
            {
                var accessor = a.GetService<IHttpContextAccessor>();
                return accessor?.HttpContext == null
                    ? new CancellationTokenSource()
                    : CancellationTokenSource.CreateLinkedTokenSource(accessor.HttpContext.RequestAborted);
            });
            return services;
        }
    }
}