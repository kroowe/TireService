using TireService.Dtos.Views.Order;

namespace TireService.Dtos.Infos.Order;

public class GetAllOrderByFilterInfo
{
    public IReadOnlyCollection<OrderStatusInfo> OrderStatuses { get; set; }
}