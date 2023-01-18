using System.ComponentModel.DataAnnotations;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities;

public class WarehouseNomenclature : BaseEntity
{
    [Required]
    public string Name { get; set; }
    public string Article { get; set; }
    public ICollection<WarehouseItemHistory> WarehouseItemHistories { get; set; }
}