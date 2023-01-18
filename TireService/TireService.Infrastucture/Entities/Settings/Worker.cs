using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TireService.Infrastructure.Entities.Base;

namespace TireService.Infrastructure.Entities.Settings;

public class Worker : BaseEntity
{
    [StringLength(256)]
    [Required]
    public string Name { get; set; }
    
    [StringLength(256)]
    [Required]
    public string Surname { get; set; }
    
    [StringLength(256)]
    [Required]
    public string Patronymic { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [Phone]
    [Required]
    public string PhoneNumber { get; set; }
    
    public DateTime? Birthday { get; set; }
    
    /// <summary>
    /// Должность
    /// </summary>
    public string Position { get; set;}
    
    public bool IsDismissed { get; set; }
    
    public WorkerBalance WorkerBalance { get; set; }
    public ICollection<PaymentRule> PaymentRules { get; set; }
    public ICollection<ShiftWorkWorker> ShiftWorkWorkers { get; set; }
    public ICollection<ShiftWork> ShiftWorks { get; set; }
    public ICollection<SalaryPaymentsToWorker> SalaryPaymentsToWorkers { get; set; }
}