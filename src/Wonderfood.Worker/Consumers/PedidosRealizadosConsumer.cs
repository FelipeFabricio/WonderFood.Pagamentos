using MassTransit;
using Wonderfood.Core.Models;
using Wonderfood.Service.Services;

namespace Wonderfood.Worker.Consumers;

public class PedidosRealizadosConsumer : IConsumer<PagamentoSolicitadoEvent>
{
    private readonly IBus _consumer;
    private readonly IPagamentoService _pagamentoService;
    
    public PedidosRealizadosConsumer(IBus consumer, IPagamentoService pagamentoService)
    {
        _consumer = consumer;
        _pagamentoService = pagamentoService;
    }
    
    public async Task Consume(ConsumeContext<PagamentoSolicitadoEvent> context)
    {
        await _pagamentoService.EnviarSolicitacaoProcessadora(context.Message);
    }
}