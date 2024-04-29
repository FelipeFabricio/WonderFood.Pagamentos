using Microsoft.AspNetCore.Mvc;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Service.Services;

namespace Wonderfood.Worker.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ProcessadoraMockController(IPagamentoService pagamentoService) : ControllerBase
    {
        /// <summary>
        /// Webhook que recebe o retorno da processadora.
        /// </summary>
        /// <response code="200">Criado com sucesso</response>
        [HttpPost(Name = "RetornoProcessadoraWebhook")]
        public async Task<IActionResult> RetornoProcessadoraWebhook([FromQuery] Guid idPedido,
            SituacaoPagamento situacao)
        {
            await pagamentoService.AtualizarStatusPagamento(idPedido, situacao);
            return Ok();
        }
    }
}