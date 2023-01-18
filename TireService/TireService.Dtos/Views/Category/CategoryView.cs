namespace TireService.Dtos.Views.Category;

public class CategoryView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public string CategoryPath { get; set; }
    public int LevelCategoryPath { get; set; }
}