using System.ComponentModel.DataAnnotations;

namespace TireService.Dtos.Infos.Order;

public class OrderUpdateInfo
{
    public Guid ShiftWorkWorkerId { get; set; }

    public string ClientComment { get; set; }
    [Required]
    public ICollection<TaskOrderInfo> TaskOrders { get; set; }
}