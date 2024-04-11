using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Core.Interfaces;

namespace Wonderfood.Worker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessadoraMockController : ControllerBase
    {
        private readonly IPagamentoRepository _repo;
        private readonly ILogger<ProcessadoraMockController> _logger;

        public ProcessadoraMockController(IPagamentoRepository repo,
            ILogger<ProcessadoraMockController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var pagamento = new Pagamento
            {
                NumeroPedido = 1,
                DataPagamento = DateTime.Now,
                CpfCliente = "12345678901",
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
    }
}