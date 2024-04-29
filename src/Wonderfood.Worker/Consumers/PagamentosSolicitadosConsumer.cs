using MassTransit;
using Wonderfood.Models.Events;
using Wonderfood.Service.Services;

namespace Wonderfood.Worker.Consumers;

public class PagamentosSolicitadosConsumer(IPagamentoService pagamentoService) : IConsumer<PagamentoSolicitadoEvent>
{
    public async Task Consume(ConsumeContext<PagamentoSolicitadoEvent> context)
    {
        await pagamentoService.EnviarSolicitacaoProcessadora(context.Message);
    }
}