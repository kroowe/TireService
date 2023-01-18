using TireService.Dtos.Views.SalaryPaymentsToWorker;

namespace TireService.Dtos.Views.Worker;

public class WorkerWithBalanceAndSalaryPaymentView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    
    /// <summary>
    /// Должность
    /// </summary>
    public string Position { get; set;}
    public bool IsDismissed { get; set; }
    
    public decimal BalanceSum { get; set; }

    public IReadOnlyCollection<SalaryPaymentsToWorkerView> SalaryPaymentsToWorkers { get; set; }
}