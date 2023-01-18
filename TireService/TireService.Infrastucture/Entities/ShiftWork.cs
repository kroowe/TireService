using TireService.Infrastructure.Entities.Base;
using TireService.Infrastructure.Entities.Settings;

namespace TireService.Infrastructure.Entities;

public class ShiftWork : BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? OpenedDate { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsOpen { get; set; }
    public DateTime? ClosedDate { get; set; }
    
    public ICollection<ShiftWorkWorker> ShiftWorkWorkers { get; set; }
    public ICollection<Worker> Workers { get; set; }
}