using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Wonderfood.Core.Interfaces;
using Wonderfood.Repository.Repositories;

namespace Wonderfood.Repository
{
    public static class DependencyInjection
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var enviroment = configuration["ASPNETCORE_ENVIRONMENT"];
            var connectionString = enviroment == "Development" ? configuration["ConnectionStrings:DefaultConnection"] : 
                configuration["MONGODB_CONNECTION"];

            var databaseName = enviroment == "Development" ? configuration["ConnectionStrings:DatabaseName"]
                : configuration["MONGO_INITDB_DATABASE"]; 

            services.AddSingleton<IMongoClient>(x => new MongoClient(connectionString));
            services.AddSingleton<IMongoDatabase>(x =>
            {
                var client = x.GetRequiredService<IMongoClient>();
                return client.GetDatabase(databaseName);
            });
            
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddHealthChecks().AddMySql(connectionString);
        }
    }
}
