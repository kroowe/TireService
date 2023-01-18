using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities.Settings;

public class Category : BaseEntity, IHaveIsDeleted
{
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public virtual Category ParentCategory { get; set; }

    [Column(TypeName = "ltree")]
    public LTree CategoryPath { get; set; }

    public ICollection<CatalogItem> CatalogItems { get; set; }
    public bool IsDeleted { get; set; }
}