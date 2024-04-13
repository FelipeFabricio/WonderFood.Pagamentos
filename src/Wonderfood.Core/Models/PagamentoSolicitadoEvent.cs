using Wonderfood.Core.Entities.Enums;

namespace Wonderfood.Core.Models;

public class PagamentoSolicitadoEvent
{
    public int NumeroPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public Guid IdCliente { get; set; }
    public DateTime DataConfirmacaoPedido { get; set; }
}