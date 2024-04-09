using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wonderfood.Repository.Interfaces;
using Wonderfood.Repository.Settings;

namespace Wonderfood.Database.Context;

public class MongoDbContext : IMongoDbContext
{
    private readonly MongoDbSettings _settings;
    private readonly MongoClient _client;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        _settings = settings.Value;
        _client = new MongoClient(_settings.ConnectionString);
    }

    public IMongoDatabase GetDatabase()
    {
        return _client.GetDatabase(_settings.DatabaseName);
    }
}