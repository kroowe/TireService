using TireService.Dtos.Infos.Warehouse;

namespace TireService.Dtos.Views.Warehouse;

public class WarehouseItemHistoryView
{
    public virtual WarehouseNomenclatureView WarehouseNomenclature { get; set; }
    public DateTime DeliveryDate { get; set; }
    public WarehouseItemHistoryTypeInfo WarehouseItemHistoryType { get; set; }
    public double Count { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal? SalePrice { get; set; }
}