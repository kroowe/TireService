using System.ComponentModel.DataAnnotations;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities;

public class WarehouseItem : BaseEntity
{
    [Required]
    public string Name { get; set; }
    public string Article { get; set; }
    [Required]
    public double Count { get; set; }
}