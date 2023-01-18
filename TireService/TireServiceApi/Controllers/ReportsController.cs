using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.Reports;
using TireService.Dtos.Views.Reports;

namespace TireServiceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly ReportsService _reportsService;
    private readonly IMapper _mapper;
    
    public ReportsController(ReportsService reportsService, IMapper mapper)
    {
        _reportsService = reportsService;
        _mapper = mapper;
    }

    [HttpPost("IncomeStatement")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<IncomeStatementReportView>))]
    public async Task<ActionResult<IReadOnlyCollection<IncomeStatementReportView>>> GetIncomeStatementReport(GetIncomeStatementReportInfo info)
    {
        var result = await _reportsService.GetIncomeStatementReport(info);
        return Ok(result);
    }

    [HttpPost("ClientOrderHistory")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<ClientOrderHistoryReportView>))]
    public async Task<ActionResult<IReadOnlyCollection<ClientOrderHistoryReportView>>> GetClientOrderHistoryReport(GetClientOrderHistoryReportInfo info)
    {
        var result = await _reportsService.GetClientOrderHistoryReport(info);
        return Ok(result);
    }

    [HttpPost("SliceCatalogItems")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<SliceCatalogItemsReportView>))]
    public async Task<ActionResult<IReadOnlyCollection<SliceCatalogItemsReportView>>> GetSliceCatalogItemsReport(GetSliceCatalogItemsReportInfo info)
    {
        var result = await _reportsService.GetSliceCatalogItemsReport(info);
        return Ok(result);
    }
}