using Microsoft.EntityFrameworkCore;
using TireService.Dtos.Infos.Client;
using TireService.Dtos.Infos.Reports;
using TireService.Dtos.Views.Order;
using TireService.Dtos.Views.Reports;
using TireService.Dtos.Views.ShiftWorkView;
using TireService.Infrastructure;
using TireService.Infrastructure.Entities;

namespace TireService.Core.Domain.Services;

public class ReportsService
{
    private readonly PostgresContext _postgresContext;
    private readonly CancellationToken _token;
    
    public ReportsService(PostgresContext postgresContext, CancellationTokenSource cancellationTokenSource)
    {
        _postgresContext = postgresContext;
        _token = cancellationTokenSource.Token;
    }

    public async Task<IReadOnlyCollection<IncomeStatementReportView>> GetIncomeStatementReport(GetIncomeStatementReportInfo info)
    {
        var ordersByPeriod = await _postgresContext.Orders.Where(x =>
                x.ClosedDate.HasValue && x.OrderStatus == OrderStatus.PaidFor &&
                x.ClosedDate.Value.Date >= info.StartDate.Value.Date &&
                x.ClosedDate.Value.Date <= info.EndDate.Value.Date)
            .Include(x => x.TaskOrders)
            .ThenInclude(x => x.CatalogItem)
            .ToListAsync(_token);
        var salaryPaidByPeriod = await _postgresContext.SalaryPaymentsToWorkers.Where(x =>
            x.PaymentDate.Date >= info.StartDate.Value.Date &&
            x.PaymentDate.Date <= info.EndDate.Value.Date)
            .ToListAsync(_token);

        var totalDay = (info.EndDate.Value - info.StartDate.Value).TotalDays + 0.5;

        var result = new List<IncomeStatementReportView>((int)totalDay);
        for (int i = 0; i < totalDay; i++)
        {
            var filterDate = info.StartDate.Value.AddDays(i).Date;
            var ordersByDate = ordersByPeriod.Where(x => x.ClosedDate.Value.Date == filterDate).ToArray();
            var taskOrdersByDate = ordersByDate.SelectMany(x => x.TaskOrders);
            var salaryPaidByDate = salaryPaidByPeriod.Where(x => x.PaymentDate.Date == filterDate);
            result.Add(new IncomeStatementReportView
            {
                Day = filterDate,
                Expenses = salaryPaidByDate.Sum(x => x.Sum),
                Revenue = taskOrdersByDate.Sum(x => x.Quantity * x.CatalogItem.Cost),
                OrderCount = ordersByDate.Length
            });
        }

        return result;
    }

    public async Task<IReadOnlyCollection<ClientOrderHistoryReportView>> GetClientOrderHistoryReport(GetClientOrderHistoryReportInfo info)
    {
        var query = _postgresContext.Clients.AsQueryable();
            
        if (string.IsNullOrEmpty(info.Name) == false)
        {
            query = query.Where(x => x.Name.Contains(info.Name));
        }

        if (string.IsNullOrEmpty(info.Surname) == false)
        {
            query = query.Where(x => x.Surname.Contains(info.Surname));
        }

        if (string.IsNullOrEmpty(info.Patronymic) == false)
        {
            query = query.Where(x => x.Patronymic.Contains(info.Patronymic));
        }

        if (string.IsNullOrEmpty(info.PhoneNumber) == false)
        {
            query = query.Where(x => x.PhoneNumber == info.PhoneNumber);
        }

        if (string.IsNullOrEmpty(info.CarNumber) == false)
        {
            query = query.Where(x => x.CarNumber.Contains(info.CarNumber));
        }

        if (info.ClientIds != null && info.ClientIds.Any())
        {
            query = query.Where(x => x.Name == info.Name);
        }

        var clients = await query
            .Include(x => x.Orders
                .Where(x => x.OrderStatus == OrderStatus.PaidFor || 
                            x.OrderStatus == OrderStatus.Cancelled))
            .ThenInclude(x => x.TaskOrders)
            .ThenInclude(x => x.CatalogItem)
            .ToListAsync(_token);

        var result = new List<ClientOrderHistoryReportView>(clients.Count);

        foreach (var client in clients)
        {
            var view = new ClientOrderHistoryReportView
            {
                Id = client.Id,
                Gender = (GenderInfo) client.Gender,
                Name = client.Name,
                Patronymic = client.Patronymic,
                Surname = client.Surname,
                CarNumber = client.CarNumber,
                PhoneNumber = client.PhoneNumber,
                PaymentSum = client.Orders
                    .Where(x => x.OrderStatus == OrderStatus.PaidFor)
                    .SelectMany(x => x.TaskOrders)
                    .Sum(x => x.Quantity * x.CatalogItem.Cost),
                OrderHistoryReports = client.Orders.Select(order => new OrderHistoryReportView
                {
                    Id = order.Id,
                    Summary = order.TaskOrders.Sum(x => x.Quantity * x.CatalogItem.Cost),
                    ClientComment = order.ClientComment,
                    CloseComment = order.CloseComment,
                    ClosedDate = order.ClosedDate.Value,
                    CreatedDate = order.CreatedDate,
                    OrderStatus = (OrderStatusInfo) order.OrderStatus
                }).ToArray()
            };
            
            result.Add(view);
        }
        return result;
    }

    public async Task<IReadOnlyCollection<SliceCatalogItemsReportView>> GetSliceCatalogItemsReport(GetSliceCatalogItemsReportInfo info)
    {
        var result = await _postgresContext.CatalogItems
            .Include(x => x.Category)
            .ThenInclude(x => x.ParentCategory)
            .Where(x => x.IsDeleted == false)
            .Select(x => new SliceCatalogItemsReportView
            {
                CatalogItemId = x.Id,
                CategoryName = x.Category.ParentCategory.Name + "." + x.Category.Name,
                CatalogItemCost = x.Cost,
                CatalogItemName = x.Name
            })
            .ToListAsync(_token);
        
        var taskOrdersByPeriod = await _postgresContext.Orders.Where(x =>
                x.ClosedDate.HasValue && x.OrderStatus == OrderStatus.PaidFor &&
                x.ClosedDate.Value.Date >= info.StartDate.Value.Date &&
                x.ClosedDate.Value.Date <= info.EndDate.Value.Date)
            .Include(x => x.TaskOrders)
            .ThenInclude(x => x.CatalogItem)
            .Select(x => x.TaskOrders)
            .ToListAsync(_token);
        var usedCatalogItemInTaskOrder = taskOrdersByPeriod
            .SelectMany(x => x)
            .GroupBy(x => x.CatalogItemId)
            .ToDictionary(x => x.Key, x => x.ToArray());

        foreach (var sliceCatalogItem in result)
        {
            if (usedCatalogItemInTaskOrder.TryGetValue(sliceCatalogItem.CatalogItemId, out var taskOrderByCatalogItem))
            {
                sliceCatalogItem.CountUsingForOrder = taskOrderByCatalogItem.Select(x => x.OrderId).Distinct().Count();
                sliceCatalogItem.TotalEarnedByCatalogItem =
                    taskOrderByCatalogItem.Sum(x => x.Quantity * x.CatalogItem.Cost);
            }
        }

        return result;
    }
}