using Wonderfood.Core.Entities.Enums;

namespace Wonderfood.Core.Models;

public class PagamentoProcessadoEvent
{
    public Guid IdPedido { get; set; }
    public SituacaoPagamento StatusPagamento { get; set; }
}