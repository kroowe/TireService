using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.SalaryPaymentsToWorker;

public class SalaryPaymentsToWorkerInfo
{
    [Required]
    public Guid? WorkerId { get; set; }
    
    [Required]
    public DateTime? PaymentDate { get; set; }
    [Required]
    public decimal? Sum { get; set; }
}