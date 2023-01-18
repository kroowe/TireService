using System.ComponentModel.DataAnnotations.Schema;
using TireService.Infrastructure.Entities.Base;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Infrastructure.Entities;

public class ShiftWorkWorker : BaseEntity
{
    public virtual Worker Worker { get; set; }
    [ForeignKey(nameof(Worker))]
    public Guid WorkerId { get; set; }
    
    public virtual ShiftWork ShiftWork { get; set; }
    [ForeignKey(nameof(ShiftWork))]
    public Guid ShiftWorkId { get; set; }
}