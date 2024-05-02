﻿using Wonderfood.Core.Entities;
using Wonderfood.Core.Entities.Enums;
using Wonderfood.Models.Events;
using Wonderfood.Repository.Repositories;
using Wonderfood.Service.Mappings;

namespace Wonderfood.Service.Services;

public class PagamentoService(IPagamentoRepository pagamentoRepository) : IPagamentoService
{
    public async Task EnviarSolicitacaoProcessadora(PagamentoSolicitadoEvent pagamento)
    {
        var pagamentoDomain = pagamento.MapToPagamento();
        await InserirPagamento(pagamentoDomain);
    }

    public async Task AtualizarStatusPagamento(Guid idPedido, SituacaoPagamento novoStatus)
    {
        await pagamentoRepository.AtualizarStatusPagamento(idPedido, new StatusPagamento
        {
            Situacao = novoStatus,
            Data = DateTime.Now
        });
    }
    
    public async Task InserirPagamento(Pagamento pagamento)
    {
        await pagamentoRepository.Inserir(pagamento);
    }
}