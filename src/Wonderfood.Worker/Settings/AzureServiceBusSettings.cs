namespace Wonderfood.Worker.QueueServices;

public class AzureServiceBusSettings
{
    public string ConnectionString { get; set; }
    public Queues Queues { get; set; }
}

public class Queues
{
    public string PagamentosProcessados { get; set; }
    public string PagamentosSolicitados { get; set; }
}