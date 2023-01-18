using System.ComponentModel.DataAnnotations.Schema;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities.Settings;

public class PaymentRule : BaseEntity
{
    [ForeignKey(nameof(Worker))]
    public Guid WorkerId { get; set; }
    public virtual Worker Worker { get; set; }
    
    public SalaryType SalaryType { get; set; }
    
    public decimal SumBySalaryType { get; set; }
}

public enum SalaryType
{
    PaymentPerDay,
    PaymentPerMonth,
    PaymentPieceWork
}