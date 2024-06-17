using MassTransit;
using Wonderfood.Core.Interfaces;
using WonderFood.Models.Events;

namespace Wonderfood.Worker.Consumers;

public class PagamentoSolicitadoConsumer(IPagamentoService pagamentoService) : IConsumer<PagamentoSolicitadoEvent>
{
    public async Task Consume(ConsumeContext<PagamentoSolicitadoEvent> context)
    {
        await pagamentoService.EnviarSolicitacaoProcessadora(context.Message);
    }
}