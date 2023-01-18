namespace TireService.Dtos.Views.Reports;

public class SliceCatalogItemsReportView
{
    public Guid CatalogItemId { get; set; }
    public string CategoryName { get; set; }
    public string CatalogItemName { get; set; }
    public decimal CatalogItemCost { get; set; }
    public decimal TotalEarnedByCatalogItem { get; set; }
    public int CountUsingForOrder { get; set; }
}