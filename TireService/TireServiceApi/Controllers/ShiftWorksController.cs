using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.CatalogItem;
using TireService.Dtos.Infos.ShiftWorks;
using TireService.Dtos.Views.CatalogItem;
using TireService.Dtos.Views.ShiftWorkView;
using TireService.Infrastructure.Entities;

namespace TireServiceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftWorksController : ControllerBase
{
    private readonly ShiftWorkService _shiftWorkService;
    private readonly AppSettingConstantService _appSettingConstantService;
    private readonly IMapper _mapper;

    public ShiftWorksController(ShiftWorkService shiftWorkService, IMapper mapper, AppSettingConstantService appSettingConstantService)
    {
        _shiftWorkService = shiftWorkService;
        _mapper = mapper;
        _appSettingConstantService = appSettingConstantService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShiftWorkView))]
    public async Task<ActionResult<ShiftWorkView>> Create([FromBody] ShiftWorkCreateInfo shiftWorkCreateInfo)
    {
        var shiftWork = _mapper.Map<ShiftWork>(shiftWorkCreateInfo);
        var appSettingConstant = await _appSettingConstantService.GetByKey(AppSettingConstantKeys.ShiftWorkDuration);
        shiftWork.Duration = TimeSpan.Parse(appSettingConstant.Value);
        await _shiftWorkService.Create(shiftWork);
        return _mapper.Map<ShiftWorkView>(shiftWork);
    }

    [HttpDelete("{shiftWorkId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete([FromRoute] Guid shiftWorkId)
    {
        var shiftWork = await _shiftWorkService.GetById(shiftWorkId, $"{nameof(ShiftWork.ShiftWorkWorkers)}");
        await _shiftWorkService.Delete(shiftWork);
        return Ok();
    }

    [HttpPost("Open/{shiftWorkId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> OpenShiftWork([FromRoute] Guid shiftWorkId)
    {
        await _shiftWorkService.OpenShiftWork(shiftWorkId);
        return Ok();
    }

    [HttpPost("Close/{shiftWorkId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CloseShiftWork([FromRoute] Guid shiftWorkId)
    {
        await _shiftWorkService.CloseShiftWork(shiftWorkId);
        return Ok();
    }

    [HttpPost("{shiftWorkId}/Worker/{workerId}/Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddWorkerToShiftWork([FromRoute] Guid shiftWorkId, [FromRoute] Guid workerId)
    {
        await _shiftWorkService.AddWorkerToShiftWork(workerId, shiftWorkId);
        return Ok();
    }

    [HttpPost("{shiftWorkId}/Worker/{workerId}/Remove")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RemoveWorkerToShiftWork([FromRoute] Guid shiftWorkId, [FromRoute] Guid workerId)
    {
        await _shiftWorkService.RemoveWorkerToShiftWork(workerId, shiftWorkId);
        return Ok();
    }

    [HttpPost("ByStartDate")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<ShiftWorkWithWorkersView>))]
    public async Task<ActionResult<IReadOnlyCollection<ShiftWorkWithWorkersView>>> GetShiftWorksByStartDate([FromBody] GetShiftWorkByStartDate info)
    {
        var shiftWorks = await _shiftWorkService.GetShiftWorksByStartDate(info.Year.Value, info.Month.Value, info.Day.Value);
        return Ok(_mapper.Map<IReadOnlyCollection<ShiftWorkWithWorkersView>>(shiftWorks));
    }

    [HttpGet("CurrentShiftWork")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShiftWorkWithWorkersView))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ShiftWorkWithWorkersView))]
    public async Task<ActionResult<ShiftWorkWithWorkersView>> GetCurrentShiftWork()
    {
        var shiftWorks = await _shiftWorkService.GetOpenShiftWork();
        return Ok(_mapper.Map<ShiftWorkWithWorkersView>(shiftWorks));
    }
}
