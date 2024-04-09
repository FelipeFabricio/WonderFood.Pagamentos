using Wonderfood.Core.Entities;
using Wonderfood.Core.Interfaces;

namespace Wonderfood.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IPagamentoRepository _pagamentoRepository;

    public Worker(ILogger<Worker> logger, IPagamentoRepository pagamentoRepository)
    {
        _logger = logger;
        _pagamentoRepository = pagamentoRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                await _pagamentoRepository.Inserir(new Pagamento());
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}