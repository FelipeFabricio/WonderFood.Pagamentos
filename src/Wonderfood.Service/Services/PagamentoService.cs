using MassTransit;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Interfaces;
using WonderFood.Models.Events;
using Wonderfood.Service.Mappings;
using StatusPagamento = Wonderfood.Core.Entities.Enums.StatusPagamento;

namespace Wonderfood.Service.Services;

public class PagamentoService(IPagamentoRepository pagamentoRepository, 
    IBus bus) : IPagamentoService
{
    public async Task EnviarSolicitacaoProcessadora(PagamentoSolicitadoEvent pagamento)
    {
        var pagamentoDomain = pagamento.MapToPagamento();
        await InserirPagamento(pagamentoDomain);
    }
    
    public async Task EnviarSolicitacaoReembolsoProcessadora(ReembolsoSolicitadoEvent reembolso)
    {
        await pagamentoRepository.AtualizarStatusPagamento(reembolso.IdPedido, new Core.Entities.StatusPagamento
        {
            Status = StatusPagamento.ReembolsoSolicitado,
            Data = DateTime.Now
        });
    }

    public async Task AtualizarStatusPagamento(Guid idPedido, StatusPagamento novoStatus)
    {
        var statusPagamento = new Core.Entities.StatusPagamento
        {
            Status = novoStatus,
            Data = DateTime.Now
        };
        
        await pagamentoRepository.AtualizarStatusPagamento(idPedido, statusPagamento);

        await bus.Publish(new PagamentoProcessadoEvent
        {
            StatusPagamento = novoStatus,
            IdPedido = idPedido
        });
    }
    
    public async Task InserirPagamento(Pagamento pagamento)
    {
        await pagamentoRepository.Inserir(pagamento);
    }

    public async Task AtualizarStatusReembolso(Guid idPedido, StatusPagamento statusReembolso)
    {
        var statusPagamento = new Core.Entities.StatusPagamento
        {
            Status = statusReembolso,
            Data = DateTime.Now
        };
        
        await pagamentoRepository.AtualizarStatusPagamento(idPedido, statusPagamento);

        await bus.Publish(new ReembolsoProcessadoEvent
        {
            StatusReembolso = statusReembolso,
            IdPedido = idPedido
        });
    }
}