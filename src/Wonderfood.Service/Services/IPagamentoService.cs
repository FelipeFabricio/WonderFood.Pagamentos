using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Models.Events;

namespace Wonderfood.Service.Services;

public interface IPagamentoService
{
    Task EnviarSolicitacaoProcessadora(PagamentoSolicitadoEvent pagamento);
    Task AtualizarStatusPagamento(Guid idPedido, SituacaoPagamento novoStatus);
    Task InserirPagamento(Pagamento pagamento);
}
