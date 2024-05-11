using MongoDB.Bson;
using MongoDB.Driver;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Interfaces;

namespace Wonderfood.Repository.Repositories;

public class PagamentoRepository(IMongoDatabase context)
    : IPagamentoRepository
{
    private readonly IMongoCollection<Pagamento> _pagamentos = context.GetCollection<Pagamento>(nameof(Pagamento));

    public async Task<List<Pagamento>> ObterTodos()
    {
        return await _pagamentos.Find(new BsonDocument()).ToListAsync();
    }
    public async Task<Pagamento> ObterPorIdPedido(Guid idPedido)
    {
        return await _pagamentos.Find(p => p.IdPedido == idPedido).FirstOrDefaultAsync();
    }

    public async Task<Pagamento> ObterPorId(Guid id)
    {
        return await _pagamentos.Find(p => p.Id == id.ToString()).FirstOrDefaultAsync();
    }

    public async Task Inserir(Pagamento pagamento)
    {
        await _pagamentos.InsertOneAsync(pagamento);
    }

    public async Task AtualizarStatusPagamento(Guid idPedido, StatusPagamento status)
    {
        var filtro = Builders<Pagamento>.Filter.Eq(p => p.IdPedido, idPedido);
        var update = Builders<Pagamento>.Update.Push(p => p.HistoricoStatus, status);

        await _pagamentos.UpdateOneAsync(filtro, update);
    }
}