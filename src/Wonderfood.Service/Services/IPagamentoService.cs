using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Core.Models;

namespace Wonderfood.Service.Services;

public interface IPagamentoService
{
    Task EnviarSolicitacaoProcessadora(PagamentoSolicitadoEvent pagamento);
    Task AtualizarStatusPagamento(Guid id, SituacaoPagamento novoStatus);
    Task InserirPagamento(Pagamento pagamento);
}
