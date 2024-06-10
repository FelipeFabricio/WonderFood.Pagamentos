using NSubstitute;
using TechTalk.SpecFlow;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Core.Interfaces;
using WonderFood.Models.Events;
using Wonderfood.Service.Services;
using Wonderfood.Worker.Webhooks;
using StatusPagamento = Wonderfood.Core.Entities.Enums.StatusPagamento;

namespace Wonderfood.BDD.Tests.Steps;

[Binding]
public class ProcessarPagamentoSteps
{
    private Pagamento _pagamento; 
    private readonly ProcessadoraWebhook _processadoraWebhook;
    private readonly IPagamentoRepository _pagamentoRepository = Substitute.For<IPagamentoRepository>();
    private readonly IPagamentoService _pagamentoService;
    private readonly IPagamentoService _pagamentoServiceMock = Substitute.For<IPagamentoService>();
    private readonly IWonderFoodPedidosExternal _pedidosExternal = Substitute.For<IWonderFoodPedidosExternal>();

    public ProcessarPagamentoSteps()
    {
        _processadoraWebhook = new ProcessadoraWebhook(_pagamentoServiceMock);
        _pagamentoService = new PagamentoService(_pagamentoRepository, _pedidosExternal);
    }
    
    [Given(@"que o Cliente possui um Pagamento com o status '(.*)'")]
    public void GivenQueOClientePossuiUmPagamentoComOStatus(string aguardandoRetornoProcessadora)
    {
        _pagamento = new Pagamento
        {
            Id = Guid.NewGuid().ToString() ,
            IdPedido = Guid.NewGuid(),
            IdCliente = Guid.NewGuid(),
            ValorTotal = 100,
            DataConfirmacaoPedido = DateTime.Now,
            FormaPagamento = FormaPagamento.Dinheiro,
            HistoricoStatus = new List<Core.Entities.StatusPagamento>
            {
                new()
                {
                    Data = DateTime.Now,
                    Status = StatusPagamento.AguardandoRetornoProcessadora
                }
            }
        };
    }

    [When(@"o sistema recebe a informação de que o pagamento foi processado com sucesso")]
    public async Task WhenOSistemaRecebeAInformacaoDeQueOPagamentoFoiProcessadoComSucesso()
    {
        //Arrange
        _pagamentoServiceMock.AtualizarStatusPagamento(Arg.Any<Guid>(), Arg.Any<StatusPagamento>()).Returns(Task.CompletedTask);
        
        //Act
        await _processadoraWebhook.RetornoProcessadora(_pagamento.IdPedido, StatusPagamento.PagamentoAprovado);
        
        //Assert
        await _pagamentoServiceMock.Received(1).AtualizarStatusPagamento(Arg.Any<Guid>(), Arg.Any<StatusPagamento>());
    }

    [Then(@"o sistema deve atualizar o status do Pagamento para '(.*)'")]
    public async Task ThenOSistemaDeveAtualizarOStatusDoPagamentoPara(string pagamentoAprovado)
    {
        //Arrange
        _pagamentoRepository.AtualizarStatusPagamento(Arg.Any<Guid>(), Arg.Any<Core.Entities.StatusPagamento>()).Returns(Task.CompletedTask);
        _pedidosExternal.EnviarPagamentoProcessado(Arg.Any<PagamentoProcessadoEvent>()).Returns(Task.CompletedTask);
        
        //Act
        await _pagamentoService.AtualizarStatusPagamento(_pagamento.IdPedido, StatusPagamento.PagamentoAprovado);
        
        //Assert
        await _pagamentoRepository.Received(1).AtualizarStatusPagamento(Arg.Any<Guid>(), Arg.Any<Core.Entities.StatusPagamento>());
    }

    [Then(@"deve comunicar ao Sistema responsável pelos Pedidos que o pagamento foi efetuado com sucesso")]
    public async Task ThenDeveComunicarAoSistemaResponsavelPelosPedidosQueOPagamentoFoiEfetuadoComSucesso()
    {
        //Assert
        await _pedidosExternal.Received(1).EnviarPagamentoProcessado(Arg.Any<PagamentoProcessadoEvent>());
    }
}