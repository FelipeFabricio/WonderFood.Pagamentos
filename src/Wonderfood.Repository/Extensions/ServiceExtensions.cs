using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wonderfood.Core.Interfaces;
using Wonderfood.Database.Context;
using Wonderfood.Repository.Interfaces;
using Wonderfood.Repository.Repositories;
using Wonderfood.Repository.Settings;

namespace Wonderfood.Repository.Extensions
{
    public static class ServiceExtensions
    {
        private static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(options => configuration.GetSection("MongoDbSettings"));
        }

        public static IServiceCollection AddMongoDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterSettings(configuration);
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();

            return services;
        }
    }
}
