using Wonderfood.Core.Entities.Enums;

namespace WonderFood.Models.Events;

public class PagamentoProcessadoEvent
{
    public Guid IdPedido { get; set; }
    public StatusPagamento StatusPagamento { get; set; }
} 