using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.CatalogItem;
using TireService.Dtos.Views.CatalogItem;
using TireService.Infrastructure.Entities.Settings;

namespace TireServiceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogItemsController : ControllerBase
    {
        private readonly CatalogItemService _catalogItemService;
        private readonly IMapper _mapper;
        
        public CatalogItemsController(CatalogItemService catalogItemService, IMapper mapper)
        {
            _catalogItemService = catalogItemService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CatalogItemView))]
        public async Task<ActionResult<CatalogItemView>> Create([FromBody] CatalogItemInfo catalogItemCreateInfo)
        {
            var catalogItemToCreate = _mapper.Map<CatalogItem>(catalogItemCreateInfo);
            await _catalogItemService.Create(catalogItemToCreate);
            return _mapper.Map<CatalogItemView>(catalogItemToCreate);
        }

        [HttpPut("{catalogItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CatalogItemView))]
        public async Task<ActionResult<CatalogItemView>> Update([FromRoute] Guid catalogItemId, [FromBody] CatalogItemInfo catalogItemUpdateInfo)
        {
            var catalogItem = await _catalogItemService.GetById(catalogItemId);
            var catalogItemToUpdate = _mapper.Map(catalogItemUpdateInfo, catalogItem);
            await _catalogItemService.Update(catalogItemToUpdate);
            return _mapper.Map<CatalogItemView>(catalogItemToUpdate);
        }
        
        [HttpDelete("{catalogItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] Guid catalogItemId)
        {
            var catalogItem = await _catalogItemService.GetById(catalogItemId);
            await _catalogItemService.Delete(catalogItem);
            return Ok();
        }

        [HttpGet("ByCategory/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<CatalogItemView>))]
        public async Task<ActionResult<IReadOnlyCollection<CatalogItemView>>> GetByCategory([FromRoute] Guid categoryId)
        {
            var catalogItems = await _catalogItemService.GetByCategory(categoryId);
            return Ok(_mapper.Map<IReadOnlyCollection<CatalogItemView>>(catalogItems));
        }
    }
}
