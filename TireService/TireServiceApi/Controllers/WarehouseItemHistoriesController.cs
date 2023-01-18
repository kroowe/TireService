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
    public class WarehouseItemHistoriesController : ControllerBase
    {
        private readonly WarehouseItemHistoryService _warehouseItemHistoryService;
        private readonly IMapper _mapper;
        
        public WarehouseItemHistoriesController(WarehouseItemHistoryService warehouseItemHistoryService, IMapper mapper)
        {
            _warehouseItemHistoryService = warehouseItemHistoryService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarehouseItemHistoryView))]
        public async Task<ActionResult<WarehouseItemHistoryView>> Create([FromBody] WarehouseItemHistoryInfo warehouseItemHistoryInfo)
        {
            var warehouseItemToCreate = _mapper.Map<WarehouseItemHistory>(warehouseItemHistoryInfo);
            await _warehouseItemHistoryService.Create(warehouseItemToCreate);
            return _mapper.Map<WarehouseItemHistoryView>(warehouseItemToCreate);
        }
        
        [HttpDelete("{warehouseItemHistoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] Guid warehouseItemHistoryId)
        {
            var warehouseItem = await _warehouseItemHistoryService.GetById(warehouseItemHistoryId);
            await _warehouseItemHistoryService.Delete(warehouseItem);
            return Ok();
        }
        

        [HttpGet("ByWarehouseNomenclature/{warehouseNomenclatureId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<WarehouseItemHistoryView>))]
        public async Task<ActionResult<IReadOnlyCollection<WarehouseItemHistoryView>>> GetByWarehouseNomenclature([FromRoute] Guid warehouseNomenclatureId)
        {
            var warehouseItemHistories = await _warehouseItemHistoryService.GetByWarehouseNomenclature(warehouseNomenclatureId);
            return Ok(_mapper.Map<IReadOnlyCollection<WarehouseItemHistoryView>>(warehouseItemHistories));
        }
    }
}
