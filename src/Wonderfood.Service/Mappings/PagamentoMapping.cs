using Wonderfood.Core.Entities;
using Wonderfood.Core.Models;

namespace Wonderfood.Service.Mappings;

public static class PagamentoMapping
{
    public static Pagamento MapToPagamento(this PagamentoSolicitadoEvent pagamentoSolicitadoEvent)
    {
        return new Pagamento
        {
            NumeroPedido = pagamentoSolicitadoEvent.NumeroPedido,
            ValorTotal = pagamentoSolicitadoEvent.ValorTotal,
            FormaPagamento = pagamentoSolicitadoEvent.FormaPagamento,
            IdCliente = pagamentoSolicitadoEvent.IdCliente,
            DataConfirmacaoPedido = pagamentoSolicitadoEvent.DataConfirmacaoPedido
        };
    } 
}