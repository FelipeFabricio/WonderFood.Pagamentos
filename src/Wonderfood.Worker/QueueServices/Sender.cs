using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Wonderfood.Worker.Interfaces;

namespace Wonderfood.Worker.QueueServices;

public class Sender : ISender
{
    private readonly AzureServiceBusSettings _settings;

    public Sender(IOptions<AzureServiceBusSettings> settings)
    {
        _settings = settings.Value;
    }
    
    public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
    {
        ServiceBusClient client;
        ServiceBusSender sender;
        
        var clientOptions = new ServiceBusClientOptions()
        { 
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        
        client = new ServiceBusClient(_settings.ConnectionString, clientOptions);
        sender = client.CreateSender(queueName);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(serviceBusMessage)));
        await sender.SendMessageAsync(message);
    }
    
}
