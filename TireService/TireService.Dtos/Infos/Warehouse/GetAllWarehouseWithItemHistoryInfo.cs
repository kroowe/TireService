namespace TireService.Dtos.Infos.Warehouse;

public class GetAllWarehouseNomenclatureInfo
{
    public string Name { get; set; }
    public string Article { get; set; }
    public IReadOnlyCollection<Guid> WarehouseNomenclatureIds { get; set; }
}