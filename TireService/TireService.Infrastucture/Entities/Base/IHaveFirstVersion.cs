namespace TireService.Infrastructure.Entities.Base;

public interface IHaveFirstVersion
{
    public Guid? FirstVersionId { get; set; }
}