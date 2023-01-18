using TireService.Dtos.Views.Client;
using TireService.Dtos.Views.Order;
using TireService.Dtos.Views.ShiftWorkView;

namespace TireService.Dtos.Views.Reports;

public class OrderHistoryReportView
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public OrderStatusInfo OrderStatus { get; set; }
    public string ClientComment { get; set; }
    public string CloseComment { get; set; }
    public DateTime ClosedDate { get; set; }
    public decimal Summary { get; set; }
}