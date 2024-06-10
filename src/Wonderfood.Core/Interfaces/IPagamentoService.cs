using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using WonderFood.Models.Events;
using StatusPagamento = Wonderfood.Core.Entities.Enums.StatusPagamento;

namespace Wonderfood.Core.Interfaces;

public interface IPagamentoService
{
    Task EnviarSolicitacaoProcessadora(PagamentoSolicitadoEvent pagamento);
    Task AtualizarStatusPagamento(Guid idPedido, StatusPagamento novoStatus);
    Task InserirPagamento(Pagamento pagamento);
}
