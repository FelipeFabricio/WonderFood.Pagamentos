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

    public async Task<Pagamento> ObterPorId(Guid id)
    {
        return await _pagamentos.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task Inserir(Pagamento pagamento)
    {
        await _pagamentos.InsertOneAsync(pagamento);
    }

    public async Task Atualizar(Pagamento pagamento)
    {
        await _pagamentos.ReplaceOneAsync(p => p.Id == pagamento.Id, pagamento);
    }

    public async Task Remover(Guid id)
    {
        await _pagamentos.DeleteOneAsync(p => p.Id == id);
    }
}