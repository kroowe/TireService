using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.Worker;
using TireService.Dtos.Views.Worker;
using TireService.Infrastructure.Entities.Settings;

namespace TireServiceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : ControllerBase
    {
        private readonly WorkerService _workerService;
        private readonly IMapper _mapper;
        
        public WorkersController(WorkerService workerService, IMapper mapper)
        {
            _workerService = workerService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkerView))]
        public async Task<ActionResult<WorkerView>> Create([FromBody] WorkerCreateInfo workerCreateInfo)
        {
            var workerToCreate = _mapper.Map<Worker>(workerCreateInfo);
            await _workerService.Create(workerToCreate);
            return _mapper.Map<WorkerView>(workerToCreate);
        }

        [HttpPut("{workerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkerView))]
        public async Task<ActionResult<WorkerView>> Update([FromRoute] Guid workerId, [FromBody] WorkerUpdateInfo workerUpdateInfo)
        {
            var worker = await _workerService.GetById(workerId);
            var workerToUpdate = _mapper.Map(workerUpdateInfo, worker);
            await _workerService.Update(workerToUpdate);
            return _mapper.Map<WorkerView>(workerToUpdate);
        }
        
        [HttpDelete("{workerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] Guid workerId)
        {
            var worker = await _workerService.GetById(workerId);
            await _workerService.Delete(worker);
            return Ok();
        }
        
        [HttpPut("{workerId}/SetDismissed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SetDismissed([FromRoute] Guid workerId, [FromBody] bool isDismissed)
        {
            await _workerService.SetDismissed(workerId, isDismissed);
            return Ok();
        }
        
        [HttpPost("filter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<WorkerView>))]
        public async Task<ActionResult<IReadOnlyCollection<WorkerView>>> GetAll([FromBody] GetAllWorkerByFilterInfo getAllWorkerByFilterInfo)
        {
            var workers = await _workerService.GetAllByFilter(getAllWorkerByFilterInfo);
            return Ok(_mapper.Map<IReadOnlyCollection<WorkerView>>(workers));
        }
        
        [HttpPost("GetAll/WithSalaryPaymentsAndBalance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<WorkerWithBalanceAndSalaryPaymentView>))]
        public async Task<ActionResult<IReadOnlyCollection<WorkerWithBalanceAndSalaryPaymentView>>> GetAllWorkerWithBalanceAndSalaryPayment([FromBody] GetAllWorkerWithBalanceAndSalaryPaymentInfo info)
        {
            var workers = await _workerService.GetAllWorkerWithBalanceAndSalaryPayment(info);
            return Ok(_mapper.Map<IReadOnlyCollection<WorkerWithBalanceAndSalaryPaymentView>>(workers));
        }
    }
}
