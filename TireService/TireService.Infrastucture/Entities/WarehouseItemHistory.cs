using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities;

public class WarehouseItemHistory : BaseEntity
{
    public virtual WarehouseNomenclature WarehouseNomenclature { get; set; }
    
    [ForeignKey(nameof(WarehouseNomenclature))]
    [Required]
    public Guid WarehouseNomenclatureId { get; set; }
    
    [Required]
    public DateTime DeliveryDate { get; set; }

    [Required] 
    public WarehouseItemHistoryType WarehouseItemHistoryType { get; set; }
    [Required]
    public double Count { get; set; }
    [Required]
    public decimal PurchasePrice { get; set; }
    public decimal? SalePrice { get; set; }
}

public enum WarehouseItemHistoryType
{
    Entrance,
    WriteOff
}