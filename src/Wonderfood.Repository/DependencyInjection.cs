using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wonderfood.Repository.Context;
using Wonderfood.Repository.Interfaces;
using Wonderfood.Repository.Repositories;

namespace Wonderfood.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            return services;
        }
    }
}
