using Microsoft.Extensions.DependencyInjection;
using Wonderfood.Service.Services;

namespace Wonderfood.Service.ServiceExtensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayerExtensions(this IServiceCollection services)
    {
        services.AddScoped<IPagamentoService, PagamentoService>();
        return services;
    }
}