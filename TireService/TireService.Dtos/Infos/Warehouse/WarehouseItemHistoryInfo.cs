using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.Warehouse;

public class WarehouseItemHistoryInfo
{
    [Required]
    public Guid? WarehouseNomenclatureId { get; set; }
    [Required]
    public WarehouseItemHistoryTypeInfo? WarehouseItemHistoryType { get; set; }
    [Required]
    public DateTime? DeliveryDate { get; set; }
    [Required]
    public double? Count { get; set; }
    [Required]
    public decimal? PurchasePrice { get; set; }
    public decimal? SalePrice { get; set; }
}

public enum WarehouseItemHistoryTypeInfo
{
    Entrance,
    WriteOff
}