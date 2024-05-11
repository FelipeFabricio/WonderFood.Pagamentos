using Wonderfood.Core.Entities.Enums;

namespace Wonderfood.Models.Events;

public class PagamentoProcessadoEvent
{
    public Guid IdPedido { get; set; }
    public StatusPagamento StatusPagamento { get; set; }
} 