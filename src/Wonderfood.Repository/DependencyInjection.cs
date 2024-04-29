using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Wonderfood.Repository.Repositories;

namespace Wonderfood.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["MONGO_CONNECTION"];
            var databaseName = configuration["MONGO_INITDB_DATABASE"];
            
            services.AddSingleton<IMongoClient>(x => new MongoClient(connectionString));
            services.AddSingleton<IMongoDatabase>(x =>
            {
                var client = x.GetRequiredService<IMongoClient>();
                return client.GetDatabase(databaseName);
            });
            
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            return services;
        }
    }
}
