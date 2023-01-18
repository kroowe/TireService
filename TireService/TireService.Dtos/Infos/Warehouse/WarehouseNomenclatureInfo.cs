using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.Warehouse;

public class WarehouseNomenclatureInfo
{    
    [Required]
    public string Name { get; set; }
    public string Article { get; set; }
}