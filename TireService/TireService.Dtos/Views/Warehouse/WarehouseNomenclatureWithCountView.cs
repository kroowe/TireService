namespace TireService.Dtos.Views.Warehouse;

public class WarehouseNomenclatureWithCountView
{
    public Guid Id { get; set; }   
    public string Name { get; set; }
    public string Article { get; set; }
    public double Count { get; set; }
}