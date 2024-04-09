using Wonderfood.Core.Entities;

namespace Wonderfood.Core.Interfaces;

public interface IPagamentoRepository
{
    Task<List<Pagamento>> ObterTodos();
    Task<Pagamento> ObterPorId(Guid id);
    Task Inserir(Pagamento pagamento);
    Task Atualizar(Pagamento pagamento);
    Task Remover(Guid id);
}