using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Wonderfood.Core.Interfaces;
using Wonderfood.ExternalServices.Services;

namespace Wonderfood.ExternalServices;

public static class DependencyInjection
{
    public static void AddExternalServices(this IServiceCollection services)
    {
        services.AddHttpClient<IWonderFoodPedidosExternal, WonderFoodPedidosExternal>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddScoped<IWonderFoodPedidosExternal, WonderFoodPedidosExternal>();
    }
}