using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities.Settings;

public class CatalogItem : BaseEntity, IHaveFirstVersion, IHaveIsDeleted
{
    public string Name { get; set; }
    public virtual Category Category { get; set; }
    
    [ForeignKey(nameof(Category))]
    [Required]
    public Guid CategoryId { get; set; }
    
    public bool IsDeleted { get; set; }
    public decimal Cost { get; set; }
    public virtual CatalogItem FirstVersion { get; set; }
    [ForeignKey(nameof(FirstVersion))]
    public Guid? FirstVersionId { get; set; }
}