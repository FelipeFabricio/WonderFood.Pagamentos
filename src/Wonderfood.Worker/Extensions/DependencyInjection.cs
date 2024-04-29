using Azure.Messaging.ServiceBus;
using MassTransit;
using Wonderfood.Models.Events;
using Wonderfood.Worker.Consumers;
using Wonderfood.Worker.QueueServices;

namespace Wonderfood.Worker.Extensions;

public static class DependencyInjection
{
    public static void AddAzureServiceBusServices(this IServiceCollection services)
    {
        var settings = services.CarregarSettings<AzureServiceBusSettings>();
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PagamentosSolicitadosConsumer>();
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(settings.ConnectionString,
                    h => { h.TransportType = ServiceBusTransportType.AmqpWebSockets; });

                cfg.ReceiveEndpoint(settings.Queues.PagamentosSolicitados, e =>
                {
                    e.ConfigureConsumer<PagamentosSolicitadosConsumer>(context);
                    e.UseMessageRetry(retryConfig => { retryConfig.Interval(3, TimeSpan.FromSeconds(5)); });
                    EndpointConvention.Map<PagamentoSolicitadoEvent>(e.InputAddress);
                });
                
                cfg.ConfigureEndpoints(context);
                cfg.UseServiceBusMessageScheduler();
            });
        });
    }
}