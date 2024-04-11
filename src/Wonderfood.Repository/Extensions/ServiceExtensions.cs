using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wonderfood.Core.Interfaces;
using Wonderfood.Database.Context;
using Wonderfood.Repository.Interfaces;
using Wonderfood.Repository.Repositories;

namespace Wonderfood.Repository.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongoDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            return services;
        }
    }
}
