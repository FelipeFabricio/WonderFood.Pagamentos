using MassTransit;
using Wonderfood.Core.Interfaces;
using WonderFood.Models.Events;

namespace Wonderfood.Worker.Consumers;

public class ReembolsoSolicitadoConsumer(IPagamentoService pagamentoService) : IConsumer<ReembolsoSolicitadoEvent>
{
    public async Task Consume(ConsumeContext<ReembolsoSolicitadoEvent> context)
    {
        await pagamentoService.EnviarSolicitacaoReembolsoProcessadora(context.Message);
    }
}