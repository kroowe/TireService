using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.Client;
using TireService.Dtos.Views.Client;
using TireService.Infrastructure.Entities;

namespace TireServiceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;
        private readonly IMapper _mapper;
        
        public ClientsController(ClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientView))]
        public async Task<ActionResult<ClientView>> Create([FromBody] ClientCreateInfo clientCreateInfo)
        {
            var clientToCreate = _mapper.Map<Client>(clientCreateInfo);
            await _clientService.Create(clientToCreate);
            return _mapper.Map<ClientView>(clientToCreate);
        }

        [HttpPut("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientView))]
        public async Task<ActionResult<ClientView>> Update([FromRoute] Guid clientId, [FromBody] ClientUpdateInfo clientUpdateInfo)
        {
            var client = await _clientService.GetById(clientId);
            var clientToUpdate = _mapper.Map(clientUpdateInfo, client);
            await _clientService.Update(clientToUpdate);
            return _mapper.Map<ClientView>(clientToUpdate);
        }
        
        [HttpDelete("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] Guid clientId)
        {
            var client = await _clientService.GetById(clientId);
            await _clientService.Delete(client);
            return Ok();
        }

        [HttpPost("filter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<ClientView>))]
        public async Task<ActionResult<IReadOnlyCollection<ClientView>>> GetAll([FromBody] GetAllClientByFilterInfo info)
        {
            var clients = await _clientService.GetAll(info);
            return Ok(_mapper.Map<IReadOnlyCollection<ClientView>>(clients));
        }
    }
}
