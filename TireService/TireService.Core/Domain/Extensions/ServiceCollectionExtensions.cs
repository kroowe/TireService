using Microsoft.Extensions.DependencyInjection;
using TireService.Core.Domain.Services;

namespace TireService.Core.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddScoped<CatalogItemService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<ClientService>();
        services.AddScoped<ClientService>();
        services.AddScoped<PaymentRuleService>();
        services.AddScoped<TaskOrderService>();
        services.AddScoped<WorkerService>();
        services.AddScoped<WorkerBalanceService>();
        services.AddScoped<WarehouseNomenclatureService>();
        services.AddScoped<WarehouseItemHistoryService>();
        services.AddScoped<AppSettingConstantService>();
        services.AddScoped<ShiftWorkService>();
        services.AddScoped<OrderService>();
        services.AddScoped<ShiftWorkWorkerService>();
        services.AddScoped<SalaryPaymentsToWorkerService>();
        
        services.AddScoped<ReportsService>();
        
    }
}