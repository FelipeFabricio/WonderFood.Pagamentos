using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wonderfood.Core.Entities;
using Wonderfood.Database.Settings;

namespace Wonderfood.Database.Context;

public class MongoDbContext
{
    public IMongoCollection<Pagamento> Pagamentos;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        Pagamentos = database.GetCollection<Pagamento>(settings.Value.CollectionName);
    }
}