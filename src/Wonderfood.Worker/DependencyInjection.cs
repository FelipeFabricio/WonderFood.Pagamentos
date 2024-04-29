using Azure.Messaging.ServiceBus;
using MassTransit;
using Wonderfood.Models.Events;
using Wonderfood.Worker.Consumers;

namespace Wonderfood.Worker;

public static class DependencyInjection
{
    public static void AddAzureServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["SERVICEBUS_CONNECTION"];
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PagamentosSolicitadosConsumer>();
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(connectionString,
                    h => { h.TransportType = ServiceBusTransportType.AmqpWebSockets; });

                cfg.ReceiveEndpoint("pagamentos-solicitados", e =>
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