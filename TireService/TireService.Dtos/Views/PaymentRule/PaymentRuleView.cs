using TireService.Dtos.Infos.PaymentRule;
using TireService.Dtos.Views.Worker;

namespace TireService.Dtos.Views.PaymentRule;

public class PaymentRuleView
{
    public Guid Id { get; set; }
    public WorkerView WorkerView { get; set; }
    
    public SalaryTypeInfo SalaryType { get; set; }
    
    public decimal SumBySalaryType { get; set; }
}