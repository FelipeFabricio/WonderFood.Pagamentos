using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Wonderfood.Core.Interfaces;
using Wonderfood.Models.Events;
using Wonderfood.Worker.Webhooks;

namespace Wonderfood.Worker.UnitTests;

public class PagamentosWebhookTests
{
    private readonly PagamentosWebhook _sut;
    private readonly IPagamentoService _pagamentoService = Substitute.For<IPagamentoService>();

    public PagamentosWebhookTests()
    {
        _sut = new PagamentosWebhook(_pagamentoService);
    }
    
    [Fact]
    [Trait("Worker", "PagamentosWebhook")]
    public void PagamentoSolicitadoWebhook_DeveRetornarOk_QuandoSolicitacaoDePagamentoForEnviadaComSucesso()
    {
        //Arrange
        var payload = new Faker<PagamentoSolicitadoEvent>("pt_BR").Generate();
        _pagamentoService.EnviarSolicitacaoProcessadora(payload).Returns(Task.CompletedTask);

        //Act
        var resultado = (OkResult)_sut.PagamentoSolicitadoWebhook(payload);

        //Assert
        resultado.StatusCode.Should().Be(200);
        _pagamentoService.Received(1).EnviarSolicitacaoProcessadora(payload);
    }

    [Fact]
    public void PagamentoSolicitadoWebhook_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        var payload = new Faker<PagamentoSolicitadoEvent>("pt_BR").Generate();
        _pagamentoService.EnviarSolicitacaoProcessadora(payload).Throws<Exception>();

        //Act
        var resultado = (BadRequestObjectResult)_sut.PagamentoSolicitadoWebhook(payload);

        //Assert
        resultado.StatusCode.Should().Be(400);
        _pagamentoService.Received(1).EnviarSolicitacaoProcessadora(payload);
    }
}