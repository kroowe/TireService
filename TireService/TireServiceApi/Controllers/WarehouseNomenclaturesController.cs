using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.Warehouse;
using TireService.Dtos.Views.Warehouse;
using TireService.Infrastructure.Entities;

namespace TireServiceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseNomenclaturesController : ControllerBase
    {
        private readonly WarehouseNomenclatureService _warehouseNomenclatureService;
        private readonly IMapper _mapper;
        
        public WarehouseNomenclaturesController(WarehouseNomenclatureService warehouseNomenclatureService, IMapper mapper)
        {
            _warehouseNomenclatureService = warehouseNomenclatureService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarehouseNomenclatureView))]
        public async Task<ActionResult<WarehouseNomenclatureView>> Create([FromBody] WarehouseNomenclatureInfo warehouseNomenclatureInfo)
        {
            var warehouseItemToCreate = _mapper.Map<WarehouseNomenclature>(warehouseNomenclatureInfo);
            await _warehouseNomenclatureService.Create(warehouseItemToCreate);
            return _mapper.Map<WarehouseNomenclatureView>(warehouseItemToCreate);
        }

        [HttpPut("{warehouseNomenclatureId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarehouseNomenclatureView))]
        public async Task<ActionResult<WarehouseNomenclatureView>> Update([FromRoute] Guid warehouseNomenclatureId, [FromBody] WarehouseNomenclatureInfo warehouseNomenclatureInfo)
        {
            var warehouseItem = await _warehouseNomenclatureService.GetById(warehouseNomenclatureId);
            var warehouseItemToUpdate = _mapper.Map(warehouseNomenclatureInfo, warehouseItem);
            await _warehouseNomenclatureService.Update(warehouseItemToUpdate);
            return _mapper.Map<WarehouseNomenclatureView>(warehouseItemToUpdate);
        }
        
        [HttpDelete("{warehouseNomenclatureId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] Guid warehouseNomenclatureId)
        {
            var warehouseItem = await _warehouseNomenclatureService.GetById(warehouseNomenclatureId);
            await _warehouseNomenclatureService.Delete(warehouseItem);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<WarehouseNomenclatureWithCountView>))]
        public async Task<ActionResult<IReadOnlyCollection<WarehouseNomenclatureWithCountView>>> Create([FromBody] GetAllWarehouseNomenclatureInfo getAllWarehouseNomenclatureInfo)
        {
            var warehouseNomenclatures = await _warehouseNomenclatureService.GetAllWarehouseNomenclatures(
                getAllWarehouseNomenclatureInfo,
                $"{nameof(WarehouseNomenclature.WarehouseItemHistories)}");
            return Ok(_mapper.Map<IReadOnlyCollection<WarehouseNomenclatureWithCountView>>(warehouseNomenclatures));
        }
    }
}
