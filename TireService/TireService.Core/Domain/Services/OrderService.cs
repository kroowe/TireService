using Microsoft.EntityFrameworkCore;
using TireService.Core.Utils;
using TireService.Dtos.Infos.Order;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities;

namespace TireService.Core.Domain.Services;

public class OrderService : ItemBasicService<Order>
{
    public const string OrderFullInclude =
        $"{nameof(Order.ShiftWorkWorker)}.{nameof(ShiftWorkWorker.Worker)},{nameof(Order.Client)},{nameof(Order.TaskOrders)}.{nameof(TaskOrder.CatalogItem)},{nameof(Order.ShiftWorkWorker)}.{nameof(ShiftWorkWorker.Worker)}";

    private readonly PaymentRuleService _paymentRuleService;
    private readonly WorkerBalanceService _workerBalanceService;
    
    public OrderService(PostgresContext dbContext, CancellationTokenSource cancellationTokenSource, WorkerBalanceService workerBalanceService, PaymentRuleService paymentRuleService) : base(dbContext, cancellationTokenSource)
    {
        _workerBalanceService = workerBalanceService;
        _paymentRuleService = paymentRuleService;
    }

    public async Task<IReadOnlyCollection<Order>> GetOrderByFilter(GetAllOrderByFilterInfo info, string include = null)
    {
        var query = _dbSet.AsQueryable();

        if (info.OrderStatuses != null && info.OrderStatuses.Any())
        {
            var orderStatuses = info.OrderStatuses.Select(x => (OrderStatus) x).ToArray();
            query = query.Where(x => orderStatuses.Contains(x.OrderStatus));
        }

        query = EntityFrameworkHelper.SetEfIncludeString(query, include);
        return await query.ToListAsync(_token);
    }
    
    public async Task<bool> CancelOrder(OrderCloseInfo info)
    {
        var order = await GetById(info.OrderId.Value);
        order.CloseComment = info.CloseComment;
        order.OrderStatus = OrderStatus.Cancelled;
        order.ClosedDate = DateTime.Now;
        await Update(order);
        return true;
    }
    
    public async Task<bool> PayOrder(Guid orderId)
    {
        var order = await _dbSet.Where(x => x.Id == orderId)
            .Include(x => x.TaskOrders)
            .ThenInclude(x => x.CatalogItem)
            .Include(x => x.ShiftWorkWorker)
            .FirstAsync(_token);
        if (order.ShiftWorkWorkerId.HasValue == false)
        {
            throw new Exception("set worker to order before paying");
        }

        order.OrderStatus = OrderStatus.PaidFor;
        order.ClosedDate = DateTime.Now;
        await Update(order);
        var sumForAddedToBalance = await _paymentRuleService.GetSumFromOrder(order.ShiftWorkWorker.WorkerId,
            order.TaskOrders.Sum(x => x.Quantity * x.CatalogItem.Cost));
        await _workerBalanceService.SumToBalance(order.ShiftWorkWorker.WorkerId, sumForAddedToBalance);
        return true;
    }
    
    public async Task<bool> TakeToWorkOrder(Guid orderId)
    {
        var order = await GetById(orderId);
        if (order.ShiftWorkWorkerId.HasValue == false)
        {
            throw new Exception("set worker to order before paying");
        }

        order.OrderStatus = OrderStatus.InWork;
        await Update(order);
        return true;
    }
}