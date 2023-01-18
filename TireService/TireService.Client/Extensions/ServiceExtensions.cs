using System;
using Microsoft.Extensions.DependencyInjection;
using TireService.Client.Contracts;
using TireService.Client.Options;

namespace TireService.Client.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTireServiceClient(this IServiceCollection services,
            Action<TireServiceClientOption> options = null)
        {
            var settings = new TireServiceClientOption();
            options?.Invoke(settings);
            services.AddSingleton(settings);
            services.AddHttpClient<ICategoriesClient, CategoriesClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.CategoriesClientPath));
            services.AddHttpClient<ICatalogItemsClient, CatalogItemsClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.CatalogItemsClientPath));
            services.AddHttpClient<IWorkersClient, WorkersClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.WorkersClientPath));
            services.AddHttpClient<IClientsClient, ClientsClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.ClientsClientPath));
            //services.AddHttpClient<IWarehouseItemsClient, WarehouseItemsClient>(a =>
              //  a.BaseAddress = new Uri(settings.BaseAddress + settings.CategoriesClientPath));
            services.AddHttpClient<IPaymentRulesClient, PaymentRulesClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.PaymentRulesClientPath));
            services.AddHttpClient<IOrdersClient, OrdersClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.OrdersClientPath));
            services.AddHttpClient<IShiftWorksClient, ShiftWorksClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.ShiftWorksClientPath));
            services.AddHttpClient<IAppSettingConstantsClient, AppSettingConstantsClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.AppSettingConstantsClientPath));
            services.AddHttpClient<ISalaryPaymentsToWorkerClient, SalaryPaymentsToWorkerClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.SalaryPaymentsToWorkerClient));
            services.AddHttpClient<IReportsClient, ReportsClient>(a =>
                a.BaseAddress = new Uri(settings.BaseAddress + settings.ReportsClientPath));

            return services;
        }
    }
}
