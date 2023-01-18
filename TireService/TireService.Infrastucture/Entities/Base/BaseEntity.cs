using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TireService.Infrastructure.Entities.Base;

public abstract class BaseEntity : DbEntity
{
    [Key]
    [NotUpdatable]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}