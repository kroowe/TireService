using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.Order;
using TireService.Dtos.Views.Order;
using TireService.Infrastructure.Entities;

namespace TireServiceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly TaskOrderService _taskOrderService;
    private readonly IMapper _mapper;
    
    public OrdersController(IMapper mapper, OrderService orderService, TaskOrderService taskOrderService)
    {
        _mapper = mapper;
        _orderService = orderService;
        _taskOrderService = taskOrderService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    public async Task<ActionResult<Guid>> Create([FromBody] OrderCreateInfo info)
    {
        var order = _mapper.Map<Order>(info);
        await _orderService.Create(order);
        var orderTasks = info.TaskOrders.Select(x => new TaskOrder
            {
                OrderId = order.Id,
                CatalogItemId = x.CatalogItemId,
                Quantity = x.Quantity
            })
            .ToArray();
        await _taskOrderService.CreateAll(orderTasks);
        return order.Id;
    }
    
    [HttpPut("{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    public async Task<ActionResult<Guid>> Update([FromRoute] Guid orderId, [FromBody] OrderUpdateInfo info)
    {
        var order = await _orderService.GetById(orderId);
        order.ShiftWorkWorkerId = info.ShiftWorkWorkerId;
        order.ClientComment = info.ClientComment;
        await _orderService.Update(order);
        var orderTasks = await _taskOrderService.GetTaskOrderByOrder(orderId);
        await _taskOrderService.DeleteAll(orderTasks);
        orderTasks = info.TaskOrders.Select(x => new TaskOrder
            {
                OrderId = order.Id,
                CatalogItemId = x.CatalogItemId,
                Quantity = x.Quantity
            })
            .ToArray();
        await _taskOrderService.CreateAll(orderTasks);
        return order.Id;
    }

    [HttpGet("{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderView))]
    public async Task<ActionResult<OrderView>> GetOrder([FromRoute] Guid orderId)
    {
        var order = await _orderService.GetById(orderId, OrderService.OrderFullInclude);
        var orderView = _mapper.Map<OrderView>(order);
        orderView.Summary = orderView.TaskOrders.Sum(x => x.Quantity * x.CatalogItem.Cost);
        return orderView;
    }

    [HttpPost("GetAll/ByFilter")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<OrderView>))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(IReadOnlyCollection<OrderView>))]
    public async Task<ActionResult<IReadOnlyCollection<OrderView>>> GetAllByFilter(GetAllOrderByFilterInfo info)
    {
        var order = await _orderService.GetOrderByFilter(info, OrderService.OrderFullInclude);
        var orderViews = _mapper.Map<IReadOnlyCollection<OrderView>>(order);

        foreach (var orderView in orderViews)
        {
            orderView.Summary = orderView.TaskOrders.Sum(x => x.Quantity * x.CatalogItem.Cost);
        }
        
        return Ok(orderViews);
    }

    [HttpGet("CountByStatuses")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderCountsView))]
    public async Task<ActionResult<OrderCountsView>> GetCountByStatuses()
    {
        var order = await _orderService.GetOrderByFilter(new GetAllOrderByFilterInfo());
        var orderCountsView = new OrderCountsView
        {
            CancelledCount = order.Count(x => x.OrderStatus == OrderStatus.Cancelled),
            CreatedCount = order.Count(x => x.OrderStatus == OrderStatus.Created),
            InWorkCount = order.Count(x => x.OrderStatus == OrderStatus.InWork),
            PaidForCount = order.Count(x => x.OrderStatus == OrderStatus.PaidFor),
        };
        
        return Ok(orderCountsView);
    }

    [HttpPost("Cancel")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<ActionResult<bool>> CancelOrder(OrderCloseInfo info)
    {
        var isClosed = await _orderService.CancelOrder(info);
        return isClosed;
    }

    [HttpPost("Pay/{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<ActionResult<bool>> PayOrder(Guid orderId)
    {
        var isPay = await _orderService.PayOrder(orderId);
        return isPay;
    }
    
    [HttpPost("TakeToWork/{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<ActionResult<bool>> TakeToWorkOrder(Guid orderId)
    {
        var isPay = await _orderService.TakeToWorkOrder(orderId);
        return isPay;
    }
}