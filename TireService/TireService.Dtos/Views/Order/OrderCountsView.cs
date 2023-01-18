namespace TireService.Dtos.Views.Order;

public class OrderCountsView
{
    public int CreatedCount { get;set; }
    public int InWorkCount { get;set; }
    public int CancelledCount { get;set; }
    public int PaidForCount { get;set; }
}