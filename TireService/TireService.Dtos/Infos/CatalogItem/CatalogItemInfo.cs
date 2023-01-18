using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.CatalogItem;

public class CatalogItemInfo
{
    public string Name { get; set; }
    
    [Required]
    public Guid? CategoryId { get; set; }
    
    public decimal Cost { get; set; }
}