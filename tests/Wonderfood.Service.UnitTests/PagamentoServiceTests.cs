using NSubstitute;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Core.Interfaces;
using WonderFood.Models.Events;
using Wonderfood.Service.Services;

namespace Wonderfood.Service.UnitTests;

public class PagamentoServiceTests
{
    private readonly PagamentoService _sut;
    private readonly IPagamentoRepository _pagamentoRepository = Substitute.For<IPagamentoRepository>();
    private readonly IWonderFoodPedidosExternal _pedidosExternal = Substitute.For<IWonderFoodPedidosExternal>();

    public PagamentoServiceTests()
    {
        _sut = new PagamentoService(_pagamentoRepository, _pedidosExternal);
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