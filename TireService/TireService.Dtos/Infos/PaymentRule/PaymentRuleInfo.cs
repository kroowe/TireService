using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.PaymentRule;

public class PaymentRuleInfo
{
    [Required]
    public Guid? WorkerId { get; set; }
    
    public SalaryTypeInfo SalaryType { get; set; }
    
    public decimal SumBySalaryType { get; set; }
}

public enum SalaryTypeInfo
{
    PaymentPerDay,
    PaymentPerMonth,
    PaymentPieceWork
}