using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Models.Events;

namespace Wonderfood.Service.Mappings;

public static class PagamentoMapping
{
    public static Pagamento MapToPagamento(this PagamentoSolicitadoEvent pagamentoSolicitadoEvent)
    {
        return new Pagamento
        {
            IdPedido = pagamentoSolicitadoEvent.IdPedido,
            ValorTotal = pagamentoSolicitadoEvent.ValorTotal,
            FormaPagamento = pagamentoSolicitadoEvent.FormaPagamento,
            IdCliente = pagamentoSolicitadoEvent.IdCliente,
            DataConfirmacaoPedido = pagamentoSolicitadoEvent.DataConfirmacaoPedido,
            HistoricoStatus =
            [
                new StatusPagamento
                {
                    Situacao = SituacaoPagamento.AguardandoRetornoProcessadora,
                    Data = DateTime.Now
                }
            ]
        };
    } 
}