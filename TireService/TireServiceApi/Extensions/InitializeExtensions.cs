using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using TireService.Core.Domain;
using TireService.Core.Domain.Services;
using TireService.Infrastructure.Entities;

namespace TireServiceApi.Extensions;

public static class InitializeExtensions
{
    public static async Task<IHost> InitAppSettingConstants(this IHost webHost)
    {
        using var scope = webHost.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var settingConstantService = services.GetRequiredService<AppSettingConstantService>();
            var d = await settingConstantService.GetByKey(AppSettingConstantKeys.ShiftWorkDuration);
            if (d == null)
            {
                await settingConstantService.CreateConstant(AppSettingConstantKeys.ShiftWorkDuration,
                    TimeSpan.FromHours(8).ToString());
            }
        }
        catch (Exception ex)
        {
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger($"{Assembly.GetEntryAssembly()?.FullName}.Program");
            logger.LogError(ex, "An error occurred while initialize app setting contants");
            throw;
        }

        return webHost;
    }
}