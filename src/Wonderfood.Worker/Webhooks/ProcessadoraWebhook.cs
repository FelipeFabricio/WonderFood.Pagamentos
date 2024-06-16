using Microsoft.AspNetCore.Mvc;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Core.Interfaces;

namespace Wonderfood.Worker.Webhooks
{
    [Route("/webhook/processadora")]
    public class ProcessadoraWebhook(IPagamentoService pagamentoService) : ControllerBase
    {
        /// <summary>
        /// Webhook que recebe o retorno da processadora.
        /// </summary>
        /// <response code="200">Criado com sucesso</response>
        [HttpPost("retorno-processadora")]
        public async Task<IActionResult> RetornoProcessadora([FromQuery] Guid idPedido,
            StatusPagamento status)
        {
            await pagamentoService.AtualizarStatusPagamento(idPedido, status);
            return Ok();
        }
        
        /// <summary>
        /// Webhook que recebe solicitação retorno da Solicitação de um pedido de Reembolso.
        /// </summary>
        /// <response code="200">Criado com sucesso</response>
        [HttpPost("retorno-reembolso-processadora")]
        public async Task<IActionResult> RetornoReembolsoProcessadora([FromQuery] Guid idPedido,
            StatusPagamento statusReembolso)
        {
            await pagamentoService.AtualizarStatusReembolso(idPedido, statusReembolso);
            return Ok();
        }
    }
}