using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.AppSettingConstant;
using TireService.Dtos.Views.AppSettingConstant;
using TireService.Infrastructure.Entities;

namespace TireServiceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AppSettingConstantsController : ControllerBase
{
    private readonly AppSettingConstantService _settingConstantService;
    private readonly IMapper _mapper;
    
    public AppSettingConstantsController(AppSettingConstantService settingConstantService, IMapper mapper)
    {
        _settingConstantService = settingConstantService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppSettingConstantView))]
    public async Task<ActionResult<AppSettingConstantView>> Create([FromBody] AppSettingConstantCreateInfo appSettingConstantCreateInfo)
    {
        var appSettingConstant = _mapper.Map<AppSettingConstant>(appSettingConstantCreateInfo);
        
        await _settingConstantService.CreateConstant(appSettingConstant.Key, appSettingConstant.Value);
        return _mapper.Map<AppSettingConstantView>(appSettingConstant);
    }

    [HttpPut("{appSettingConstantKey}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppSettingConstantView))]
    public async Task<ActionResult<AppSettingConstantView>> Update([FromRoute] string appSettingConstantKey,
        [FromBody] AppSettingConstantUpdateInfo appSettingConstantUpdateInfo)
    {
        var appSettingConstant = await _settingConstantService.UpdateConstant(appSettingConstantKey, appSettingConstantUpdateInfo.Value);
        return _mapper.Map<AppSettingConstantView>(appSettingConstant);
    }

    [HttpDelete("{appSettingConstantKey}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete([FromRoute] string appSettingConstantKey)
    {
        await _settingConstantService.RemoveConstant(appSettingConstantKey);
        return Ok();
    }
}