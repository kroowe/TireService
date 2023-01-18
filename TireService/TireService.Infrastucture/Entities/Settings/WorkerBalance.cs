using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities.Settings;

public class WorkerBalance : BaseEntity
{
    [ForeignKey(nameof(Worker))]
    [Required]
    public Guid WorkerId { get; set; }
    public virtual Worker Worker { get; set; }
    
    [Required]
    public decimal Balance { get; set; }
}