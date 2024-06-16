using Wonderfood.Core.Entities.Enums;

namespace WonderFood.Models.Events;

public class ReembolsoProcessadoEvent
{
    public Guid IdPedido { get; set; }
    public StatusPagamento StatusReembolso { get; set; }
} 