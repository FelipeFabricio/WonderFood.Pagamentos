using MassTransit;
using MongoDB.Bson.Serialization.Serializers;
using NSubstitute;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Core.Interfaces;
using WonderFood.Models.Events;
using Wonderfood.Service.Services;
using StatusPagamento = Wonderfood.Core.Entities.StatusPagamento;

namespace Wonderfood.Service.UnitTests;

public class PagamentoServiceTests
{
    private readonly PagamentoService _sut;
    private readonly IPagamentoRepository _pagamentoRepository = Substitute.For<IPagamentoRepository>();
    private readonly IBus _bus = Substitute.For<IBus>();

    public PagamentoServiceTests()
    {
        _sut = new PagamentoService(_pagamentoRepository, _bus);
    }

    [Fact]
    [Trait("Service", "Pagamento")]
    public async Task EnviarSolicitacaoProcessadora_DeveInserirPagamento_QuandoDadosPagamentoForemValidos()
    {
        //Arrange
        var pagamentoEvent = GerarPagamentoSolicitadoEventValido();
        
        _pagamentoRepository.Inserir(Arg.Any<Pagamento>()).Returns(Task.CompletedTask);
        _sut.InserirPagamento(Arg.Any<Pagamento>()).Returns(Task.CompletedTask);
        
        //Act
        await _sut.EnviarSolicitacaoProcessadora(pagamentoEvent);
        
        //Assert
        await _pagamentoRepository.Received(1).Inserir(Arg.Any<Pagamento>());
    }
    
    [Fact]
    [Trait("Service", "Pagamento")]
    public async Task EnviarSolicitacaoReembolsoProcessadora_DeveAtualizarStatusPagamento_QuandoDadosReembolsoForemValidos()
    {
        //Arrange
        var reembolsoEvent = new ReembolsoSolicitadoEvent
        {
            IdPedido = Guid.NewGuid()
        };
        
        _pagamentoRepository.AtualizarStatusPagamento(Arg.Any<Guid>(), Arg.Any<StatusPagamento>()).Returns(Task.CompletedTask);
        
        //Act
        await _sut.EnviarSolicitacaoReembolsoProcessadora(reembolsoEvent);
        
        //Assert
        await _pagamentoRepository.Received(1).AtualizarStatusPagamento(Arg.Any<Guid>(), Arg.Any<StatusPagamento>());
    }
    
    [Fact]
    [Trait("Service", "Pagamento")]
    public async Task AtualizarStatusReembolso_DeveAtualizarStatus_QuandoDadosForemValidos()
    {
        //Arrange
        var idPedido = Guid.NewGuid();
        var novoStatus = Core.Entities.Enums.StatusPagamento.ReembolsoAprovado;
        
        _pagamentoRepository.AtualizarStatusPagamento(Arg.Any<Guid>(), Arg.Any<StatusPagamento>()).Returns(Task.CompletedTask);
        
        //Act
        await _sut.AtualizarStatusReembolso(idPedido, novoStatus);
        
        //Assert
        await _pagamentoRepository.Received(1).AtualizarStatusPagamento(Arg.Any<Guid>(), Arg.Any<StatusPagamento>());
    }

    private PagamentoSolicitadoEvent GerarPagamentoSolicitadoEventValido()
    {
        return new PagamentoSolicitadoEvent
        {
            IdPedido = Guid.NewGuid(),
            IdCliente = Guid.NewGuid(),
            ValorTotal = 100,
            FormaPagamento = FormaPagamento.CartaoCredito,
            DataConfirmacaoPedido = DateTime.Now
        };
    }
}