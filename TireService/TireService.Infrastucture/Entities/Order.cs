using System.ComponentModel.DataAnnotations.Schema;
using TireService.Infrastructure.Entities.Base;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Infrastructure.Entities;

public class Order : BaseEntity
{
    [ForeignKey(nameof(ClientId))]
    public virtual Client Client { get; set; }
    public Guid ClientId { get; set; }
    
    public virtual ShiftWorkWorker ShiftWorkWorker { get; set; }
    
    [ForeignKey(nameof(ShiftWorkWorker))]
    public Guid? ShiftWorkWorkerId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public OrderStatus OrderStatus { get; set; }
    public string ClientComment { get; set; }
    public string CloseComment { get; set; }
    public DateTime? ClosedDate { get; set; }

    public ICollection<TaskOrder> TaskOrders { get; set; }
}

public enum OrderStatus
{
    Created,
    InWork,
    Cancelled,
    PaidFor
}