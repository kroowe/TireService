using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.SalaryPaymentsToWorker;
using TireService.Dtos.Views.SalaryPaymentsToWorker;
using TireService.Dtos.Views.ShiftWorkView;
using TireService.Infrastructure.Entities;
using TireService.Infrastructure.Entities.Settings;

namespace TireServiceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SalaryPaymentsToWorkerController : ControllerBase
{
    private readonly SalaryPaymentsToWorkerService _salaryPaymentsToWorkerService;
    private readonly IMapper _mapper;
    
    public SalaryPaymentsToWorkerController(SalaryPaymentsToWorkerService salaryPaymentsToWorkerService, IMapper mapper)
    {
        _salaryPaymentsToWorkerService = salaryPaymentsToWorkerService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    public async Task<ActionResult<Guid>> Create([FromBody] SalaryPaymentsToWorkerInfo salaryPaymentsToWorkerInfo)
    {
        var salaryPaymentsToWorker = _mapper.Map<SalaryPaymentsToWorker>(salaryPaymentsToWorkerInfo);
        await _salaryPaymentsToWorkerService.Create(salaryPaymentsToWorker);
        return salaryPaymentsToWorker.Id;
    }

    [HttpDelete("{salaryPaymentsToWorkerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete([FromRoute] Guid salaryPaymentsToWorkerId)
    {
        await _salaryPaymentsToWorkerService.Delete(salaryPaymentsToWorkerId);
        return Ok();
    }
}