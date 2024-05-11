using Microsoft.AspNetCore.Mvc;
using Wonderfood.Core.Interfaces;
using Wonderfood.Models.Events;

namespace Wonderfood.Worker.Webhooks;

[Route("/webhook/pagamentos")]
public class PagamentosWebhook(IPagamentoService pagamentoService) : ControllerBase
{
    /// <summary>
    /// Webhook que recebe solicitação de processamento de pagamento.
    /// </summary>
    /// <response code="200">Criado com sucesso</response>
    [HttpPost("pagamento-solicitado")]
    public IActionResult PagamentoSolicitadoWebhook([FromBody] PagamentoSolicitadoEvent payload)
    {
        try
        {
            pagamentoService.EnviarSolicitacaoProcessadora(payload);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(400, $"Erro ao processar o webhook: {ex.Message}");
        }
    }
}