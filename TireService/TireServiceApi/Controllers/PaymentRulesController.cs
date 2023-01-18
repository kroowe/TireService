using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TireService.Core.Domain.Services;
using TireService.Dtos.Infos.PaymentRule;
using TireService.Dtos.Views.PaymentRule;
using TireService.Infrastructure.Entities.Settings;

namespace TireServiceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentRulesController : ControllerBase
    {
        private readonly PaymentRuleService _paymentRuleService;
        private readonly IMapper _mapper;
        
        public PaymentRulesController(PaymentRuleService paymentRuleService, IMapper mapper)
        {
            _paymentRuleService = paymentRuleService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentRuleView))]
        public async Task<ActionResult<PaymentRuleView>> Create([FromBody] PaymentRuleInfo paymentRuleCreateInfo)
        {
            var paymentRule = _mapper.Map<PaymentRule>(paymentRuleCreateInfo);
            await _paymentRuleService.Create(paymentRule);
            paymentRule = await _paymentRuleService.GetById(paymentRule.Id, $"{nameof(PaymentRule.Worker)}");
            return _mapper.Map<PaymentRuleView>(paymentRule);
        }

        [HttpPut("{paymentRuleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentRuleView))]
        public async Task<ActionResult<PaymentRuleView>> Update([FromRoute] Guid paymentRuleId, [FromBody] PaymentRuleInfo paymentRuleUpdateInfo)
        {
            var paymentRule = await _paymentRuleService.GetById(paymentRuleId, $"{nameof(PaymentRule.Worker)}");
            var workerToUpdate = _mapper.Map(paymentRuleUpdateInfo, paymentRule);
            await _paymentRuleService.Update(workerToUpdate);
            return _mapper.Map<PaymentRuleView>(workerToUpdate);
        }
        
        [HttpDelete("{paymentRuleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] Guid paymentRuleId)
        {
            var paymentRule = await _paymentRuleService.GetById(paymentRuleId);
            await _paymentRuleService.Delete(paymentRule);
            return Ok();
        }
        

        [HttpGet("ByWorker/{workerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<PaymentRuleView>))]
        public async Task<ActionResult<IReadOnlyCollection<PaymentRuleView>>> GetByWorker([FromRoute] Guid workerId)
        {
            var paymentRules = await _paymentRuleService.GetByWorker(workerId);
            return Ok(_mapper.Map<IReadOnlyCollection<PaymentRuleView>>(paymentRules));
        }
    }
}
