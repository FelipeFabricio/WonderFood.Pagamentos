using Microsoft.AspNetCore.Mvc;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Service.Services;

namespace Wonderfood.Worker.Controllers
{
    [ApiController]
    [Route("[controller]/webhook")]
    public class WebhookProcessadoraController(IPagamentoService pagamentoService) : ControllerBase
    {
        /// <summary>
        /// Webhook que recebe o retorno da processadora.
        /// </summary>
        /// <response code="200">Criado com sucesso</response>
        [HttpPost("retorno-processadora")]
        public async Task<IActionResult> RetornoProcessadora([FromQuery] Guid idPedido,
            SituacaoPagamento situacao)
        {
            await pagamentoService.AtualizarStatusPagamento(idPedido, situacao);
            return Ok();
        }
    }
}