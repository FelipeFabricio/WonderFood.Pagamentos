using MongoDB.Bson;
using MongoDB.Driver;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Interfaces;
using Wonderfood.Database.Context;

namespace Wonderfood.Database.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly IMongoCollection<Pagamento> _pagamentos;

    public PagamentoRepository(MongoDbContext context)
    {
        _pagamentos = context.Pagamentos;
    }
    
    public async Task<List<Pagamento>> ObterTodos()
    {
        return await _pagamentos.Find(new BsonDocument()).ToListAsync();
    }

    public Task<Pagamento> ObterPorId(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Inserir(Pagamento pagamento)
    {
        await _pagamentos.InsertOneAsync(pagamento);
    }

    public Task Atualizar(Pagamento pagamento)
    {
        throw new NotImplementedException();
    }

    public Task Remover(Guid id)
    {
        throw new NotImplementedException();
    }
}