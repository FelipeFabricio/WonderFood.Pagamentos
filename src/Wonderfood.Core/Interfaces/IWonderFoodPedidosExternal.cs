using Wonderfood.Models.Events;

namespace Wonderfood.Core.Interfaces;

public interface IWonderFoodPedidosExternal
{
    Task EnviarPagamentoProcessado(PagamentoProcessadoEvent pagamento);
}