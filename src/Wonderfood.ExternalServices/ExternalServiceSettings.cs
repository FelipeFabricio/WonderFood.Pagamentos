namespace Wonderfood.ExternalServices;

public class ExternalServicesSettings
{
    public WonderfoodPedido WonderfoodPedidos { get; set; }
}

public class WonderfoodPedido
{
    public string BaseUrl { get; set; }
    public string PagamentoProcessado { get; set; }
}
