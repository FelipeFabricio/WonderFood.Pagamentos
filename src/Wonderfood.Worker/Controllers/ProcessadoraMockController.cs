using System.Text.Json;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Core.Models;
using Wonderfood.Repository.Repositories;
using Wonderfood.Service.Services;
using Wonderfood.Worker.Interfaces;

namespace Wonderfood.Worker.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ProcessadoraMockController : ControllerBase
    {
        private readonly IPagamentoRepository _repo;
        private readonly ILogger<ProcessadoraMockController> _logger;
        private readonly IPublisher _publisher;
        private readonly IPagamentoService _pagamentoService;

        public ProcessadoraMockController(IPagamentoRepository repo,
            ILogger<ProcessadoraMockController> logger,  IPublisher publisher, IPagamentoService pagamentoService)
        {
            _repo = repo;
            _logger = logger;
            _publisher = publisher;
            _pagamentoService = pagamentoService;
        }

        /// <summary>
        /// Webhook que recebe o retorno da processadora.
        /// </summary>
        /// <response code="200">Criado com sucesso</response>
        [HttpPost(Name = "RetornoProcessadoraWebhook")]
        public async Task<IActionResult> RetornoProcessadoraWebhook([FromQuery] Guid idPedido, SituacaoPagamento situacao)
        {
            await _pagamentoService.AtualizarStatusPagamento(idPedido, situacao);
            return Ok();
        }
        
        
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var pagamento = new Pagamento
            {
                NumeroPedido = 1,
                DataConfirmacaoPedido = DateTime.Now,
                IdCliente = Guid.NewGuid(),
                ValorTotal = 100,
                FormaPagamento = FormaPagamento.CartaoCredito,
                HistoricoStatus = new List<StatusPagamento>
                {
                    new StatusPagamento
                    {
                        Data = DateTime.Now,
                        Situacao = SituacaoPagamento.PagamentoAprovado
                    },
                    new StatusPagamento
                    {
                        Data = DateTime.Now.AddDays(-1),
                        Situacao = SituacaoPagamento.AguardandoRetornoProcessadora
                    },
                }
            };
            
            await _repo.Inserir(pagamento);
            var retorno = await _repo.ObterTodos();

            var response = JsonSerializer.Serialize(retorno);

            return Ok(response);
        }
        
        [HttpPost(Name = "Publicar")]
        public async Task<IActionResult> GetDeNovo()
        {
            // var evento = new PagamentoRecusadoEvent
            // {
            //     Recusado = true
            // };
            //
            // var evento2 = new PagamentoConfirmadoEvent()
            // {
            //     Confirmado = true
            // };
            //
            var evento3 = new PagamentoSolicitadoEvent()
            {
                NumeroPedido = 1,
                DataConfirmacaoPedido = DateTime.Now,
                IdCliente = Guid.NewGuid(),
                ValorTotal = 100,
                FormaPagamento = FormaPagamento.CartaoCredito
            };
            
            // await _publisher.Send(evento);
            // await _publisher.Send(evento2);
            await _publisher.Send(evento3);
            
            return Ok();
        }
    }
}