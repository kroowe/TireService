namespace TireService.Dtos.Views.CatalogItem;

public class CatalogItemView
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public decimal Cost { get; set; }
}