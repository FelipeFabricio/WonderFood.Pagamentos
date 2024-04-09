using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Interfaces;

namespace Wonderfood.Worker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IPagamentoRepository _repo;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IPagamentoRepository repo,
            ILogger<WeatherForecastController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var pagamento = new Pagamento();
            await _repo.Inserir(pagamento);
            var retorno = await _repo.ObterTodos();

            var response = JsonSerializer.Serialize(retorno);

            return Ok(response);
        }
    }
}