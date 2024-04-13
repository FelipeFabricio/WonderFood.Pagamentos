using Wonderfood.Core.Entities;

namespace Wonderfood.Repository.Repositories;

public interface IPagamentoRepository
{
    Task<List<Pagamento>> ObterTodos();
    Task<Pagamento> ObterPorNumeroPedido(int numeroPedido);
    Task<Pagamento> ObterPorId(Guid id);
    Task Inserir(Pagamento pagamento);
    Task AtualizarStatusPagamento(Guid id, StatusPagamento status);
}