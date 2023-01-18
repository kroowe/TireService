using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace TireServiceApi.Extensions;

public static class DatabaseMigratorExtensions
{
    public static async Task<IHost> MigrateDatabase<TDbContext>(this IHost webHost) where TDbContext : DbContext
    {
        using var scope = webHost.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var dbContext = services.GetRequiredService<TDbContext>();
            await dbContext.Database.MigrateAsync();

            if (dbContext.Database.GetDbConnection() is NpgsqlConnection npgsqlConnection)
            {
                var cancellationTokenSource = new CancellationTokenSource();
                await npgsqlConnection.OpenAsync(cancellationTokenSource.Token);
                npgsqlConnection.ReloadTypes();
                await npgsqlConnection.CloseAsync();
            }

            else
                throw new ApplicationException($"dbConnection is not {nameof(NpgsqlConnection)}!");
        }
        catch (Exception ex)
        {
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger($"{Assembly.GetEntryAssembly()?.FullName}.Program");
            logger.LogError(ex, "An error occurred while migrating the database");
            throw;
        }

        return webHost;
    }
}