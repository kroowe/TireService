using System.ComponentModel.DataAnnotations.Schema;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities.Settings;

public class SalaryPaymentsToWorker : BaseEntity
{
    [ForeignKey(nameof(Worker))]
    public Guid WorkerId { get; set; }
    public virtual Worker Worker { get; set; }
    
    public DateTime PaymentDate { get; set; }
    public decimal Sum { get; set; }
}