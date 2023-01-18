using TireService.Dtos.Views.Client;
using TireService.Dtos.Views.ShiftWorkView;
using TireService.Dtos.Views.Worker;

namespace TireService.Dtos.Views.Order;

public class OrderView
{
    public Guid Id { get; set; }
    public ClientView Client { get; set; }
    public ShiftWorkWorkerView ShiftWorkWorker { get; set; }
    public IReadOnlyCollection<TaskOrderView> TaskOrders { get; set; }
    public DateTime CreatedDate { get; set; }
    public OrderStatusInfo OrderStatus { get; set; }
    public string ClientComment { get; set; }
    public string CloseComment { get; set; }
    public DateTime? ClosedDate { get; set; }
    public decimal Summary { get; set; }
}

public enum OrderStatusInfo
{
    Created,
    InWork,
    Cancelled,
    PaidFor
}