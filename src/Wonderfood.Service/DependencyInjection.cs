using Microsoft.Extensions.DependencyInjection;
using Wonderfood.Core.Interfaces;
using Wonderfood.Service.Services;

namespace Wonderfood.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IPagamentoService, PagamentoService>();
        return services;
    }
}