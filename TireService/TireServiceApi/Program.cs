using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TireService.Infrastructure;
using TireServiceApi.Extensions;

namespace TireServiceApi
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var build = CreateHostBuilder(args).Build();

            await build.MigrateDatabase<PostgresContext>();
            await build.InitAppSettingConstants();
            await build.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}