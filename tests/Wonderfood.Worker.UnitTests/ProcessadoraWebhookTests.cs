using NSubstitute;
using Wonderfood.Core.Interfaces;
using Wonderfood.Worker.Webhooks;

namespace Wonderfood.Worker.UnitTests;

public class ProcessadoraWebhookTests
{
    private readonly IPagamentoService _pagamentoService = Substitute.For<IPagamentoService>();
    private readonly ProcessadoraWebhook _sut;
    
    public ProcessadoraWebhookTests()
    {
        _sut = new ProcessadoraWebhook(_pagamentoService);
    }
    
    [Fact]
    public async Task RetornoReembolsoProcessadora_DeveAtualizarStatusReembolso()
    {
        // Arrange
        var idPedido = Guid.NewGuid();
        var statusReembolso = Core.Entities.Enums.StatusPagamento.ReembolsoAprovado;
        
        // Act
        await _sut.RetornoReembolsoProcessadora(idPedido, statusReembolso);
        
        // Assert
        await _pagamentoService.Received(1).AtualizarStatusReembolso(idPedido, statusReembolso);
    }
}