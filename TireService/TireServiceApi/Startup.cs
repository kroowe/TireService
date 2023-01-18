using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TireService.Core.Domain.Extensions;
using TireService.Infrastructure;
using TireServiceApi.Extensions;
using TireServiceApi.Handlers;
using TireServiceApi.Infrastructure;
using TireServiceApi.Tools;

namespace TireServiceApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<HttpContextFacade>();
            services.AddScoped<ITransactionMediator, TransactionMediator>();
            services.AddDomainServices();

            var connectionString = "Host=localhost;Port=5432;Database=database_name;Username=postgres;Password=password";
            var executeCommandTimeout = Configuration.GetValue<int?>("DB_EXECUTE_COMMAND_TIMEOUT") ?? 120;
            services.AddDbContextPool<PostgresContext>(options => options.UseNpgsql(connectionString, builder =>
            {
                builder.CommandTimeout((int)TimeSpan.FromMinutes(executeCommandTimeout).TotalSeconds);
            }));

            services.AddAutoMapper();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipeLineBehavior<,>));
            HttpContextAccessorServiceCollectionExtensions.AddHttpContextAccessor(services);
            services.AddSwaggerDocument(options =>
            {
                options.Title = "TireService API";
                options.Description = "TireService API Documentation";
                options.Version = "1.0.0";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGlobalExceptionsMiddleware();
            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
