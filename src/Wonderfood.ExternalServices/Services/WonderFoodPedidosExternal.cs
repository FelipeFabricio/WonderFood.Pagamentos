using Microsoft.Extensions.Options;
using Wonderfood.Core.Interfaces;
using Wonderfood.Models.Events;

namespace Wonderfood.ExternalServices.Services;

public class WonderFoodPedidosExternal(HttpClient httpClient, IOptions<ExternalServicesSettings> settings) 
    : ExternalServiceBase, IWonderFoodPedidosExternal
{
    private readonly ExternalServicesSettings _settings = settings.Value;

    public async Task EnviarPagamentoProcessado(PagamentoProcessadoEvent pagamento)
    {
        var url = string.Concat(_settings.WonderfoodPedidos.BaseUrl, _settings.WonderfoodPedidos.PagamentoProcessado);
        using var response = await httpClient.PostAsync(url, CreateStringContent(pagamento));
    }
}