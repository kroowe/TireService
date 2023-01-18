using TireService.Dtos.Views.Worker;

namespace TireService.Dtos.Views.SalaryPaymentsToWorker;

public class SalaryPaymentsToWorkerView
{
    public Guid Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Sum { get; set; }
}