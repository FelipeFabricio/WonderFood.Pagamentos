using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Wonderfood.Core.Entities;
using Wonderfood.Core.Interfaces;
using Wonderfood.Repository.Interfaces;
using Wonderfood.Repository.Settings;

namespace Wonderfood.Repository.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly IMongoCollection<Pagamento> _pagamentos;
    private readonly MongoDbSettings _settings;

    public PagamentoRepository(IMongoDbContext context, IOptions<MongoDbSettings> settings)
    {
        _settings = settings.Value;
        _pagamentos = context.GetDatabase().GetCollection<Pagamento>(_settings.CollectionName);
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