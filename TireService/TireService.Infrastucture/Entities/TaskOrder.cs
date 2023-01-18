using System.ComponentModel.DataAnnotations.Schema;
using TireService.Infrastructure.Entities.Base;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Infrastructure.Entities;

public class TaskOrder : BaseEntity
{
    public virtual Order Order { get; set; }
    
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    
    public virtual CatalogItem CatalogItem { get; set; }
    
    [ForeignKey(nameof(CatalogItem))]
    public Guid CatalogItemId { get; set; }
    
    public int Quantity { get; set; }
}