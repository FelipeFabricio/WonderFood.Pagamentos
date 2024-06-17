using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using WonderFood.Models.Events;
using StatusPagamento = Wonderfood.Core.Entities.Enums.StatusPagamento;

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
                new Core.Entities.StatusPagamento
                {
                    Status = StatusPagamento.AguardandoRetornoProcessadora,
                    Data = DateTime.Now
                }
            ]
        };
    } 
}