namespace TireService.Client.Options
{
    public class TireServiceClientOption
    {
        public string BaseAddress { get; set; }

        public string CatalogItemsClientPath { get; set; } = "CatalogItems";
        public string CategoriesClientPath { get; set; } = "Categories";
        public string ClientsClientPath { get; set; } = "Clients";
        public string PaymentRulesClientPath { get; set; } = "PaymentRules";
        public string WarehouseItemsClientPath { get; set; } = "WarehouseItems";
        public string WorkersClientPath { get; set; } = "Workers";
        public string OrdersClientPath { get; set; } = "Orders";
        public string AppSettingConstantsClientPath { get; set; } = "AppSettingConstants";
        public string SalaryPaymentsToWorkerClient { get; set; } = "SalaryPaymentsToWorker";
        public string ReportsClientPath { get; set; } = "Reports";
        public string ShiftWorksClientPath { get; set; } = "ShiftWorks";
    }
}
