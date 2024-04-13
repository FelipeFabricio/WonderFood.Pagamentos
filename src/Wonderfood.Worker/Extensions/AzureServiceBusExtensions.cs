using MassTransit;
using Wonderfood.Core.Models;
using Wonderfood.Worker.Consumers;
using Wonderfood.Worker.Interfaces;
using Wonderfood.Worker.QueueServices;

namespace Wonderfood.Worker.Extensions;

public static class AzureServiceBusExtensions
{
    public static void AddAzureServiceBusConsumers(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO: Refatorar
        var settings = services.CarregarSettings<AzureServiceBusSettings>();
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PedidosRealizadosConsumer>();
            x.UsingAzureServiceBus((context, cfg) =>
            {
                //TODO: Verificar configurações da fila
                cfg.UseConcurrencyLimit(1);
                cfg.AutoDeleteOnIdle = TimeSpan.FromDays(1);

                cfg.Host(settings.ConnectionString,
                    h => { h.TransportType = Azure.Messaging.ServiceBus.ServiceBusTransportType.AmqpWebSockets; });

                cfg.ReceiveEndpoint(settings.Queues.PagamentosSolicitados, e =>
                {
                    e.ConfigureConsumer<PedidosRealizadosConsumer>(context);

                    //TODO: Verificar qual dessas opções faz sentido para o projeto
                    e.PrefetchCount = 100;
                    e.ConcurrentMessageLimit = 100;
                    e.LockDuration = TimeSpan.FromMinutes(5);
                    e.MaxAutoRenewDuration = TimeSpan.FromMinutes(30);
                    EndpointConvention.Map<PagamentoSolicitadoEvent>(e.InputAddress);
                });
            });
        });
    }

    public static void AddAzureServiceBusPublisher(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = services.CarregarSettings<AzureServiceBusSettings>();
        services.AddMassTransit<IPublisher>(x =>
        {
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(settings.ConnectionString,
                    h => { h.TransportType = Azure.Messaging.ServiceBus.ServiceBusTransportType.AmqpWebSockets; });
                
                cfg.ReceiveEndpoint(settings.Queues.PagamentosConfirmados, e =>
                {
                    EndpointConvention.Map<PagamentoConfirmadoEvent>(e.InputAddress);
                });
                
                cfg.ReceiveEndpoint(settings.Queues.PagamentosRecusados, e =>
                {
                    EndpointConvention.Map<PagamentoRecusadoEvent>(e.InputAddress);
                });
                cfg.UseServiceBusMessageScheduler();
            });
        });
    }
}