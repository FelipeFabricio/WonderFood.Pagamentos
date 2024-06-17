using Wonderfood.Core.Entities;
using WonderFood.Models.Events;
using StatusPagamento = Wonderfood.Core.Entities.Enums.StatusPagamento;

namespace Wonderfood.Core.Interfaces;

public interface IPagamentoService
{
    Task InserirPagamento(Pagamento pagamento);
    Task EnviarSolicitacaoProcessadora(PagamentoSolicitadoEvent pagamento);
    Task EnviarSolicitacaoReembolsoProcessadora(ReembolsoSolicitadoEvent reembolso);
    Task AtualizarStatusPagamento(Guid idPedido, StatusPagamento novoStatus);
    Task AtualizarStatusReembolso(Guid idPedido, StatusPagamento statusReembolso);
}
