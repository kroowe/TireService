using TireService.Dtos.Views.CatalogItem;

namespace TireService.Dtos.Views.Order;

public class TaskOrderView
{
    public Guid Id { get; set; }
    public CatalogItemView CatalogItem { get; set; }
    
    public int Quantity { get; set; }
}