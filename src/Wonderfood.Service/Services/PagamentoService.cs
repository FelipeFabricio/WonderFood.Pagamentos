using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Core.Models;
using Wonderfood.Repository.Repositories;
using Wonderfood.Service.Mappings;

namespace Wonderfood.Service.Services;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoRepository _pagamentoRepository;

    public PagamentoService(IPagamentoRepository pagamentoRepository)
    {
        _pagamentoRepository = pagamentoRepository;
    }

    public async Task EnviarSolicitacaoProcessadora(PagamentoSolicitadoEvent pagamento)
    {
        var pagamentoDomain = pagamento.MapToPagamento();
        pagamentoDomain.HistoricoStatus.Add(new StatusPagamento
        {
            Situacao = SituacaoPagamento.AguardandoRetornoProcessadora,
            Data = DateTime.Now
        });
        await InserirPagamento(pagamentoDomain);
    }

    public async Task AtualizarStatusPagamento(Guid id, SituacaoPagamento novoStatus)
    {
        await _pagamentoRepository.AtualizarStatusPagamento(id, new StatusPagamento
        {
            Situacao = novoStatus,
            Data = DateTime.Now
        });
    }

    public async Task InserirPagamento(Pagamento pagamento)
    {
        await _pagamentoRepository.Inserir(pagamento);
    }
}