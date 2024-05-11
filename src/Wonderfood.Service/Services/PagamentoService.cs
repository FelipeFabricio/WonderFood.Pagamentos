using Wonderfood.Core.Entities;
using Wonderfood.Core.Interfaces;
using Wonderfood.Models.Events;
using Wonderfood.Service.Mappings;
using StatusPagamento = Wonderfood.Core.Entities.Enums.StatusPagamento;

namespace Wonderfood.Service.Services;

public class PagamentoService(IPagamentoRepository pagamentoRepository, IWonderFoodPedidosExternal pedidosExternal) : IPagamentoService
{
    public async Task EnviarSolicitacaoProcessadora(PagamentoSolicitadoEvent pagamento)
    {
        var pagamentoDomain = pagamento.MapToPagamento();
        await InserirPagamento(pagamentoDomain);
    }

    public async Task AtualizarStatusPagamento(Guid idPedido, StatusPagamento novoStatus)
    {
        var statusPagamento = new Core.Entities.StatusPagamento
        {
            Status = novoStatus,
            Data = DateTime.Now
        };
        
        await pagamentoRepository.AtualizarStatusPagamento(idPedido, statusPagamento);

        await pedidosExternal.EnviarPagamentoProcessado(new PagamentoProcessadoEvent
        {
            StatusPagamento = novoStatus,
            IdPedido = idPedido
        });
    }
    
    public async Task InserirPagamento(Pagamento pagamento)
    {
        await pagamentoRepository.Inserir(pagamento);
    }
}